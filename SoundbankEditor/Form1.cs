using EASoundbankTools.Model;
using EASoundbankTools.Parser;
using System.Diagnostics;
using System.Media;

namespace MaddenMixer
{
    public partial class MaddenMixer : Form
    {
        private Logger log;
        private SoundPlayer player;
        private int currentlyPlayingRow = -1;

        private readonly string LOADED_SBR_PATH = Path.Combine(Directory.GetCurrentDirectory(), "Temp\\loaded_file.sbr");
        private readonly string LOADED_SBS_PATH = Path.Combine(Directory.GetCurrentDirectory(), "Temp\\loaded_file.sbs");

        private enum EDITOR_STATE
        {
            NO_FILE_LOADED,
            INVALID_FILE_LOADED,
            SBR_LOADED_NEED_SBS,
            SBR_SBS_LOADED,
            SBR_STANDALONE_LOADED
        }

        private enum MUSIC_STATE
        {
            PLAYING,
            STOPPED
        }

        private enum DATAGRID_COLUMNS : int
        {
            Name = 0,
            Offset = 1,
            Codec = 2,
            Replace = 3,
            Play = 4
        }

        private EDITOR_STATE editorState = EDITOR_STATE.NO_FILE_LOADED;
        private MUSIC_STATE musicState = MUSIC_STATE.STOPPED;

        private ISoundbank soundbank;

        public MaddenMixer()
        {
            log = new Logger();

            InitializeComponent();
            InitializeFileSystem();
            InitializeSoundPlayer();
        }

        private void InitializeFileSystem()
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Temp"));
        }

        private void InitializeSoundPlayer()
        {
            player = new SoundPlayer();
        }

        private void btnOpenSbr_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.SuspendLayout();

            if (openSoundFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.Delete(LOADED_SBR_PATH);
                File.Delete(LOADED_SBS_PATH);

                if (musicState == MUSIC_STATE.PLAYING)
                {
                    StopPlayingMusic();
                }

                try
                {
                    SoundbankParser parser = new SoundbankParser();
                    soundbank = parser.ParseSbr(openSoundFileDialog.FileName);

                    if (soundbank is Soundbank_SbrSbs)
                    {
                        editorState = EDITOR_STATE.SBR_LOADED_NEED_SBS;
                    }
                    else
                    {
                        editorState = EDITOR_STATE.SBR_STANDALONE_LOADED;
                    }

                    File.Copy(openSoundFileDialog.FileName, LOADED_SBR_PATH, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    log.AddToLog(ex.Message);
                    editorState = EDITOR_STATE.INVALID_FILE_LOADED;
                }
                finally
                {
                    lbl_FileName.Text = openSoundFileDialog.FileName;
                    lbl_FileName.Visible = true;

                    OnEditorStateChange();
                    flowLayoutPanel1.ResumeLayout();
                }
            }
        }

        private void OnEditorStateChange()
        {
            switch (editorState)
            {
                default:
                case EDITOR_STATE.NO_FILE_LOADED:
                    soundbankEntryBindingSource.DataSource = null;
                    toolStripStatusLabel1.Text = "Ready";
                    SetControlColumnsVisibility(false);
                    break;

                case EDITOR_STATE.INVALID_FILE_LOADED:
                    btnOpenSbs.Visible = false;
                    soundbankEntryBindingSource.DataSource = null;

                    toolStripStatusLabel1.Text = "Invalid File";
                    SetControlColumnsVisibility(false);
                    break;

                case EDITOR_STATE.SBR_LOADED_NEED_SBS:
                    btnOpenSbs.Visible = true;
                    soundbankEntryBindingSource.DataSource = soundbank.Entries;

                    toolStripStatusLabel1.Text = "Loaded SBR. Need SBS file...";
                    SetControlColumnsVisibility(false);
                    break;

                case EDITOR_STATE.SBR_STANDALONE_LOADED:
                    btnOpenSbs.Visible = false;
                    soundbankEntryBindingSource.DataSource = soundbank.Entries;

                    toolStripStatusLabel1.Text = "Loaded Standalone SBR Successfully";
                    SetControlColumnsVisibility(true);
                    break;

                case EDITOR_STATE.SBR_SBS_LOADED:
                    soundbankEntryBindingSource.DataSource = soundbank.Entries;

                    toolStripStatusLabel1.Text = "Loaded SBR and SBS Successfully";
                    SetControlColumnsVisibility(true);
                    break;
            }
        }

        private void SetControlColumnsVisibility(bool visible)
        {
            dataGridView1.Columns[(int)DATAGRID_COLUMNS.Codec].Visible = visible;
            dataGridView1.Columns[(int)DATAGRID_COLUMNS.Replace].Visible = visible;
            dataGridView1.Columns[(int)DATAGRID_COLUMNS.Play].Visible = visible;
        }

        private void btnOpenSbs_Click(object sender, EventArgs e)
        {
            if (openSoundFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.Delete(LOADED_SBS_PATH);

                ((Soundbank_SbrSbs)soundbank).SbsPath = openSoundFileDialog.FileName;
                SoundbankParser parser = new SoundbankParser();
                parser.AddEAAudioHeaderData(soundbank, openSoundFileDialog.FileName);

                editorState = EDITOR_STATE.SBR_SBS_LOADED;

                File.Copy(openSoundFileDialog.FileName, LOADED_SBS_PATH, true);
                OnEditorStateChange();
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridView1.CurrentCell.ColumnIndex)
            {
                case (int)DATAGRID_COLUMNS.Replace:
                    if (IsEditorReady())
                    {
                        if (openMp3Dialog.ShowDialog() == DialogResult.OK)
                        {
                            await ConvertMp3(openMp3Dialog.FileName);
                        }
                    }

                    break;
                case (int)DATAGRID_COLUMNS.Play:
                    if (IsEditorReady())
                    {
                        if (musicState == MUSIC_STATE.STOPPED)
                        {
                            await LoadAndPlaySong();
                        }
                        else
                        {
                            if (dataGridView1.CurrentCell.RowIndex == currentlyPlayingRow)
                            {
                                StopPlayingMusic();
                                toolStripStatusLabel1.Text = "Ready";
                            }
                            else
                            {
                                ResetPlayButton(currentlyPlayingRow);
                                await LoadAndPlaySong();
                            }
                        }
                    }

                    break;
            }
        }

        private bool IsEditorReady()
        {
            return editorState == EDITOR_STATE.SBR_STANDALONE_LOADED 
                || editorState == EDITOR_STATE.SBR_SBS_LOADED;
        }

        private async Task ConvertMp3(string mp3FilePath)
        {
            toolStripStatusLabel1.Text = "Converting MP3 file...";
            ((DataGridViewButtonCell)dataGridView1.CurrentCell).UseColumnTextForButtonValue = false;
            dataGridView1.CurrentCell.Value = "Working...";

            var job = await MusicConverter.ConvertMp3ToSbs(mp3FilePath);
            log.AddToLog(job.StandardOutput);
            log.AddToLog(job.ErrorOutput);

            logOutput.Text = log.GetLogAsString();
            ScrollToBottomOfLog();

            if (job.Status)
            {
                toolStripStatusLabel1.Text = "Conversion successful. Replacing...";
                log.AddToLog("MP3 file conversion successful.");

                var WriteOutputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Temp\\conversion_write_result");

                // Replace here
                SoundbankWriter writer = new SoundbankWriter();
                writer.WriteAndFixOffsets(soundbank, soundbank.Entries[dataGridView1.CurrentCell.RowIndex], job.OutputPath, WriteOutputFolder);

                File.Copy(WriteOutputFolder + "_RAM.chunk", WriteOutputFolder + ".sbr", true);
                File.Copy(WriteOutputFolder + "_STREAM.chunk", WriteOutputFolder + ".sbs", true);

                toolStripStatusLabel1.Text = "Song replaced.";
                ResetReplaceButton(dataGridView1.CurrentRow.Index);
            }
            else
            {
                ResetReplaceButton(dataGridView1.CurrentRow.Index);
                toolStripStatusLabel1.Text = "Failed to convert MP3 file, check log";
            }
        }

        private void ResetReplaceButton(int row)
        {
            dataGridView1.Rows[row].Cells[(int)DATAGRID_COLUMNS.Replace].Value = "Replace...";
        }

        private async Task LoadAndPlaySong()
        {
            toolStripStatusLabel1.Text = "Loading song...";

            ((DataGridViewButtonCell)dataGridView1.CurrentCell).UseColumnTextForButtonValue = false;
            dataGridView1.CurrentCell.Value = "Loading...";

            var songToPlayIndex = dataGridView1.CurrentCell.RowIndex;

            var job = await WavGenerator.GenerateFromFileAsync(LOADED_SBR_PATH, songToPlayIndex + 1);
            log.AddToLog(job.StandardOutput);
            log.AddToLog(job.ErrorOutput);

            logOutput.Text = log.GetLogAsString();
            ScrollToBottomOfLog();

            if (job.Status)
            {
                player.SoundLocation = job.OutputPath;
                player.Load();
                player.Play();

                musicState = MUSIC_STATE.PLAYING;

                currentlyPlayingRow = songToPlayIndex;
                toolStripStatusLabel1.Text = $"Playing song #{ songToPlayIndex }...";

                dataGridView1.CurrentCell.Value = "Stop";
            }
            else
            {
                ResetPlayButton(songToPlayIndex);
                toolStripStatusLabel1.Text = "Failed to load song, check log";
            }
        }

        private void ScrollToBottomOfLog()
        {
            logOutput.SelectionStart = logOutput.Text.Length;
            logOutput.ScrollToCaret();
        }

        private void ResetPlayButton(int row)
        {
            dataGridView1.Rows[row].Cells[(int)DATAGRID_COLUMNS.Play].Value = "Play";
        }

        private void StopPlayingMusic()
        {
            player.Stop();

            ResetPlayButton(currentlyPlayingRow);
            currentlyPlayingRow = -1;

            musicState = MUSIC_STATE.STOPPED;
        }
    }
}