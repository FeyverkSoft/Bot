using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using Neuro.Interface;
using Neuro.InterfaceImpl;
using Neuro.Properties;
using Neuro.Domain;

namespace Neuro
{
    public partial class Form1 : Form
    {
        private Thread _teachThread; //поток в котором происходит обучение
        private readonly Thread _formThread;//поток который перерисовывает форму

        private Int32 _neuronCount = 2; //колличество нейронов в персептроне
        private Int32 _imageWidth = 1920;
        private Int32 _imageHeight = 1080;

        private IPerceptron _perceptron;
        private ITeacher _teacher;
        public Form1()
        {
            InitializeComponent();
            //Первая цифра колличество нейронов.
            //Вторая цифра колличество элементов векторном представлении картинки
            //Для размера картинки 64x64 будет произведение
            _perceptron = new Perceptron(_neuronCount, _imageWidth * _imageHeight);
            _teacher = new Teacher(_perceptron);
            pictureBox1.Image = new Bitmap(_imageWidth, _imageHeight);
            _formThread = new Thread(UpdateForm);
            _formThread.Start();
            label6.Text = "v: " + ProductVersion;
        }

        private void UpdateForm()
        {
            while (true)
            {
                if (_teachThread != null)
                    if (_teachThread.ThreadState == ThreadState.Running)
                    {
                        if (menuStrip1.Enabled)
                        {
                            foreach (var control in Controls.OfType<Button>())
                            {
                                control.Invoke(new Action(() => control.Enabled = false));
                            }
                            numericUpDown1.Invoke(new Action(() => numericUpDown1.Enabled = false));
                            progressBar1.Invoke(new Action(() => progressBar1.Visible = true));
                            menuStrip1.Invoke(new Action(() => menuStrip1.Enabled = false));
                        }
                    }
                    else
                    {
                        if (!menuStrip1.Enabled)
                        {
                            foreach (var control in Controls.OfType<Button>())
                            {
                                control.Invoke(new Action(() => control.Enabled = true));
                            }
                            numericUpDown1.Invoke(new Action(() => numericUpDown1.Enabled = true));
                            progressBar1.Invoke(new Action(() => progressBar1.Visible = false));
                            menuStrip1.Invoke(new Action(() => menuStrip1.Enabled = true));
                        }
                    }
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Обучает сеть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (label4.Text == "")
                {
                    using (var folder = new FolderBrowserDialog { Description = RU.Form1_button1_Click_Выберете_папку_с_картинками_для_обучения })
                    {
                        if (folder.ShowDialog() == DialogResult.OK)
                        {
                            label4.Text = folder.SelectedPath;
                        }
                    }
                }
                if (!String.IsNullOrEmpty(label4.Text) && !String.IsNullOrWhiteSpace(label4.Text))
                {
                    _teachThread = new Thread(() =>
                    {
                        _teacher.Teach(LoadImage(label4.Text, 1), (Int32)numericUpDown1.Value);
                        //  Application.Exit();
                    });
                    _teachThread.Start();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// загружаемые образы
        /// </summary>
        /// <param name="path"> Путь для загрузки образов</param>
        /// <param name="count">Колличество символов учавствующих в названии файла обозначающие класс изображения</param>
        /// <returns></returns>
        private ImageData[] LoadImage(String path, Int32 count)
        {
            // загрузка всех тестовых изображений в массив bitmaps[]
            var list = Directory.GetFiles(path, "*.jpg").ToList();
            list.AddRange(Directory.GetFiles(path, "*.png"));
            var images = new List<ImageData>();
            Parallel.ForEach(list, (item) =>
            {
                using (var map = new Bitmap(item))
                {
                    var img = new ImageData
                    {
                        Data = ImageToArray(map),
                        Class = Convert.ToInt32((Path.GetFileNameWithoutExtension(item).Substring(0, count)))
                    };
                    lock (images)
                        images.Add(img);
                }
            });
            return images.ToArray();
        }

        /// <summary>
        /// Распознаёт картинку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var output = _perceptron.Recognize(ImageToArray((Bitmap)pictureBox1.Image));
                var res = new float[_perceptron.GetNeuronCount];
                foreach (var t in output)
                {
                    for (var j = 0; j < res.Length; j++)
                        res[j] += t[j] / output.Length;
                }
                var max = res.Max();
                for (var i = 0; i < res.Length; i++)
                {
                    if (!(Math.Abs(res[i] - max) < 0.0009)) continue;
                    label2.Text = i.ToString(CultureInfo.InvariantCulture);
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        byte[] ConvertBitmapToArray(Bitmap img)
        {
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            System.Drawing.Imaging.BitmapData tempData =
                  img.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                  img.PixelFormat);
            IntPtr ptr = tempData.Scan0;
            int bytes = img.Width * img.Height * 3;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            img.UnlockBits(tempData);
            return rgbValues;
        }

        /// <summary>
        /// Преобразует картинку в целочисленный массив с пороговой функцией цвета
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private float[][] ImageToArray(Bitmap bitmap)
        {
            var pixel = new float[4][];
            var k = 0;
            for (var i = 0; i < 4; i++)
                pixel[i] = new float[bitmap.Height * bitmap.Width];
            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    var color = bitmap.GetPixel(i, j);
                    pixel[0][k] = color.R;
                    pixel[1][k] = color.G;
                    pixel[2][k] = color.B;
                    pixel[3][k] = Step(color);
                    k++;
                }
           }
            return pixel;
        }

        private Int32 Step(Color c)
        {
            var pixel = c.R < 100 ? 1 : 0;
            if (pixel != 1)
                return c.G < 100 ? 1 : 0;
            if (pixel != 1)
                return c.B < 100 ? 1 : 0;
            return pixel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog { Title = RU.Form1_button3_Click_Выберете_образ_для_распознования, Filter = "Jpg|*.jpg|BMP|*.bmp" })
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(open.FileName);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var folder = new FolderBrowserDialog { Description = RU.Form1_button1_Click_Выберете_папку_с_картинками_для_обучения })
            {
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    label4.Text = folder.SelectedPath;
                }
            }
        }

        private void saveDataBasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var save = new SaveFileDialog { Title = Resources.Form1_saveDataBasesToolStripMenuItem_Click_Выберете_базу_данных, Filter = "База Данных|*.IIDB" })
                {
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        using (var str = File.Create(save.FileName))
                        {
                            var bf = new BinaryFormatter();
                            bf.Serialize(str, new SaveStructure
                            {
                                Perceptron = _perceptron,
                                ImageHeight = _imageHeight,
                                ImageWidth = _imageWidth,
                                NeuronCount = _neuronCount
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void loadDataBasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var open = new OpenFileDialog { Title = Resources.Form1_saveDataBasesToolStripMenuItem_Click_Выберете_базу_данных, Filter = "База Данных|*.IIDB" })
                {
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        using (var str = File.OpenRead(open.FileName))
                        {
                            var bf = new BinaryFormatter();
                            var temp = (SaveStructure)bf.Deserialize(str);
                            _perceptron = temp.Perceptron;
                            _teacher = new Teacher(_perceptron);
                            _imageHeight = temp.ImageHeight;
                            _imageWidth = temp.ImageWidth;
                            _neuronCount = temp.NeuronCount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sett = new settingsForm(_neuronCount, _imageWidth, _imageHeight);
            sett.ShowDialog();
            if (!sett.ItsClosed)
            {
                _imageHeight = sett.GetImageHeight;
                _imageWidth = sett.GetImageWidth;
                _neuronCount = sett.GetNeuronCount;
                _perceptron = new Perceptron(_neuronCount, _imageWidth * _imageHeight);
                _teacher = new Teacher(_perceptron);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Feyverk[Kill]Soft aka Mazin Peter \r\nVersion: " + ProductVersion + "\r\nName: Обучаемый персептрон для распознавания образов");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_formThread != null)
                    _formThread.Abort();
                if (_teachThread != null)
                    _teachThread.Abort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
