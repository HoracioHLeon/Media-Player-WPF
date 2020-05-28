using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Espacio de nombres de la clase DispatcherTimer.
using System.Windows.Threading;

// Espacio de nombres de la clase Directory
using System.IO;

// Espacio de nombres de la clase OpenFileDialog
using Microsoft.Win32;

namespace Music_Player
{
    /// <summary>
    /// Lógica de interacción para Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        #region Campos auxiliares

        ListBoxItem currentTrack;
        ListBoxItem PreviousTrack;
        Brush currentTrackIndicator;
        Brush TrackColor;
        DispatcherTimer timer;
        bool isDragging;
        //bool MediaElementIsNowPlaying = false;
        bool MediaElementIsPaused = false;

        #endregion

        //Constructor
        public Player()
        {
            this.InitializeComponent();
            btn_pause.Visibility = Visibility.Hidden;
            // Insert code required on object creation below this point.
            currentTrack = null;
            PreviousTrack = null;
            currentTrackIndicator = Brushes.Violet;
            TrackColor = listaDeReproduccion.Foreground; // ListBoxItem.Foreground es diferente

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Tick se dispara cada segundo.
            timer.Tick += new EventHandler(timer_Tick);
            isDragging = false;
        }

        #region Manejadores de eventos

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging) // Si NO hay operación de arrastre en el sliderTimeLine, su posición se actualiza cada segundo.
            {
                sliderTimeLine.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void listaDeReproduccion_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }               
        }

        private void listaDeReproduccion_Drop(object sender, DragEventArgs e)
        {
            try
            {
                string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                foreach (string fileName in s)
                {
                    if (System.IO.Path.GetExtension(fileName) == ".mp3" ||
                        System.IO.Path.GetExtension(fileName) == ".MP3" ||
                        System.IO.Path.GetExtension(fileName) == ".wmv" ||
                        System.IO.Path.GetExtension(fileName) == ".WMV")
                    {
                        ListBoxItem lstItem = new ListBoxItem();
                        lstItem.Content = System.IO.Path.GetFileNameWithoutExtension(fileName);
                        lstItem.Tag = fileName;
                        listaDeReproduccion.Items.Add(lstItem);
                    }
                }
                if (currentTrack == null)//es el primer drag and drop.
                {
                    listaDeReproduccion.SelectedIndex = 0;
                    PlayOrPauseTrack();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mePlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            sliderTimeLine.Value = 0;
            MoveToNextTrack();
        }

        private void mePlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mePlayer.NaturalDuration.TimeSpan;
            sliderTimeLine.Maximum = ts.TotalSeconds;
            lbl_total_time.Content = ts.Duration();
            timer.Start();
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            btn_play.Visibility = Visibility.Hidden;
            btn_pause.Visibility = Visibility.Visible;
            PlayOrPauseTrack();
        }

        private void btn_pause_Click(object sender, RoutedEventArgs e)
        {
            btn_play.Visibility = Visibility.Visible;
            btn_pause.Visibility = Visibility.Hidden;
            Pausa();

        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            btn_play.Visibility = Visibility.Visible;
            btn_pause.Visibility = Visibility.Hidden;
        }

        private void sliderTimeLine_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mePlayer.Position = TimeSpan.FromSeconds(sliderTimeLine.Value);
        }

        private void sliderTimeLine_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void sliderTimeLine_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            isDragging = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliderTimeLine.Value);// La posición del MediaElement se actualiza para que coincida con el progreso del sliderTimeLine.
        }

        private void sliderTimeLine_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lbl_timer.Content = mePlayer.Position = TimeSpan.FromSeconds(sliderTimeLine.Value);
        }

        private void btn_adelante_Click(object sender, RoutedEventArgs e)
        {
            if (listaDeReproduccion.HasItems)
            {
                MoveToNextTrack();
            }
        }

        private void btn_atras_Click(object sender, RoutedEventArgs e)
        {
            if (listaDeReproduccion.HasItems)
            {
                MoveToPrecedentTrack();
            }
        }

        private void btn_music_Click(object sender, RoutedEventArgs e)
        {
            AbrirArchivos();
        }

        private void listaDeReproduccion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AbrirArchivos();
        }

        #endregion

        #region Métodos

        private void AbrirArchivos()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            fd.Multiselect = true;
            fd.Title = "Seleccione archivos de audio mp3 y video wmv";
            fd.Filter = "Archivos Multimedia mp3 y wmv (*.mp3),(*.wmv)|*.mp3;*.wmv|Audio mp3 (*.mp3)|*.mp3|video wmv (*.wmv)|*.wmv";

            try
            {
                Nullable<bool> result = fd.ShowDialog();

                if (result == true)
                {

                    Stop();
                    mePlayer.Source = null;
                    listaDeReproduccion.Items.Clear();

                    string[] selectedFiles = fd.FileNames;
                    foreach (string file in selectedFiles)
                    {
                        ListBoxItem lstItem = new ListBoxItem();
                        lstItem.Content = System.IO.Path.GetFileNameWithoutExtension(file);
                        lstItem.Tag = file;
                        listaDeReproduccion.Items.Add(lstItem);
                    }
                    listaDeReproduccion.SelectedIndex = 0;
                    PlayOrPauseTrack();
                    btn_play.Visibility = Visibility.Hidden;
                    btn_pause.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PlayOrPauseTrack()// Se puede hacer un "tipo" de PerformClick en el botón Play pero lo dejaré así por que es más sencillo.
        {
            if (listaDeReproduccion.HasItems)
            {
               Play();
            }
        }

        private void Play()
        {


            if (!MediaElementIsPaused) // Nos movimos a otra pista
            {
                if (currentTrack != null) // Si anteriormente se estaba reproduciendo una pista
                {
                    PreviousTrack = currentTrack;
                    PreviousTrack.Foreground = TrackColor; // Le devolvemos su color de fuente original
                }
                currentTrack = (ListBoxItem)listaDeReproduccion.SelectedItem;
                currentTrack.Foreground = currentTrackIndicator;

                mePlayer.Source = new Uri(currentTrack.Tag.ToString()); // Aqui se hace el cambio de pista.                
                sliderTimeLine.Value = 0;
            }

            mePlayer.Play();
            MediaElementIsPaused = false;
            /*btn_play.Visibility = Visibility.Collapsed;
            btn_pause.Visibility = Visibility.Visible;*/
            //MediaElementIsNowPlaying = true;
        }

        private void Pausa()
        {
            /*btn_pause.Visibility = Visibility.Collapsed;
            btn_play.Visibility = Visibility.Visible;*/
            mePlayer.Pause();
            MediaElementIsPaused = true;
            //MediaElementIsNowPlaying = false;
        }

        private void Stop()
        {
            if (listaDeReproduccion.HasItems)
            {
                /*btn_play.Visibility = Visibility.Visible;
                btn_pause.Visibility = Visibility.Collapsed;*/
                mePlayer.Stop();
                sliderTimeLine.Value = 0; // esto es por un problema de retraso, el slider no vuelve completamente al principio inmediatamente aun no doy por qué.
                MediaElementIsPaused = false;
                //MediaElementIsNowPlaying = false;
            }
        }

        private void MoveToNextTrack() // Resolver el problema de que cuando doy next o prev el botón play no muestra el path correcto.
        {
            if (listaDeReproduccion.Items.IndexOf(currentTrack) < listaDeReproduccion.Items.Count - 1)
            {
                listaDeReproduccion.SelectedIndex = listaDeReproduccion.Items.IndexOf(currentTrack) + 1;
                MediaElementIsPaused = false;
                Play();
                return;
            }
            else if (listaDeReproduccion.Items.IndexOf(currentTrack) == listaDeReproduccion.Items.Count - 1)
            {
                listaDeReproduccion.SelectedIndex = 0;
                MediaElementIsPaused = false;
                Play();
                return;
            }

        }

        private void MoveToPrecedentTrack()
        {
            if (listaDeReproduccion.Items.IndexOf(currentTrack) > 0)
            {
                listaDeReproduccion.SelectedIndex = listaDeReproduccion.Items.IndexOf(currentTrack) - 1;
                MediaElementIsPaused = false;
                Play();
            }
            else if (listaDeReproduccion.Items.IndexOf(currentTrack) == 0)
            {
                listaDeReproduccion.SelectedIndex = listaDeReproduccion.Items.Count - 1;
                MediaElementIsPaused = false;
                Play();
            }

        }


        #endregion
    }
}
