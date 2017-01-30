using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ImgComparer.Helpers;
using ImgComparer.Neuro.Domain;
using ImgComparer.Neuro.Interface;
using ImgComparer.Neuro.InterfaceImpl;
using WpfConverters.Extensions.Commands;

namespace ImgComparer.Model
{
    public class MainWindowModel : BaseViewModel
    {
        private String _posPath;
        private String _negPath;

        public String PosPath
        {
            get { return _posPath; }
            set
            {
                _posPath = value;
                OnPropertyChanged();
            }
        }

        public String NegPath
        {
            get { return _negPath; }
            set
            {
                _negPath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбор папки с позитивными образцами
        /// </summary>
        private ICommand _selectPosPathCommand;
        /// <summary>
        /// Выбор папки с негативными образцами
        /// </summary>
        private ICommand _selectNegPathCommand;
        /// <summary>
        /// Запустить обучение
        /// </summary>
        private ICommand _runCommand;

        /// <summary>
        /// Команда выбор папки с позитивными образцами
        /// </summary>
        public ICommand SelectPosPathCommand => _selectPosPathCommand ?? (_selectPosPathCommand = new DelegateCommand(SelectPosPathMethod));
        /// <summary>
        /// Команда выбор папки с негативными образцами
        /// </summary>
        public ICommand SelectNegPathCommand => _selectNegPathCommand ?? (_selectNegPathCommand = new DelegateCommand(SelectNegPathMethod));
        /// <summary>
        /// Команда выбор папки с негативными образцами
        /// </summary>
        public ICommand RunCommand => _runCommand ?? (_runCommand = new DelegateCommand(RunCommandMethod));

        private void RunCommandMethod()
        {
            if (PosPath == null || NegPath == null)
                throw new Exception("Выбирете директории образцов");

            if (PosPath.Equals(NegPath, StringComparison.InvariantCultureIgnoreCase))
                throw new Exception("Папка с негативными и позитивными образцами не может совпадать.");
            var posList = new List<String>();
            var negList = new List<String>();

            foreach (var s in "*.png|*.jpg|*.bmp|*.jpeg".Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries))
            {
                posList.AddRange(Directory.GetFiles(PosPath, s));
            }
            foreach (var s in "*.png|*.jpg|*.bmp|*.jpeg".Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                negList.AddRange(Directory.GetFiles(NegPath, s));
            }

            if (posList.Count == 0)
                throw new Exception("Отсутсвуют позитивные корректные образцы, png,jpg,bmp,jpeg ");

            if (negList.Count == 0)
                throw new Exception("Отсутсвуют негативные корректные образцы, png,jpg,bmp,jpeg ");

            Int32 width, height;
            using (var bmmp = new Bitmap(posList.First()))
            {
                width = bmmp.Width;
                height = bmmp.Height;
            }

            var sempls = new List<ImageData>();
            sempls.AddRange(GetSemp(posList, "pos", width, height));
            sempls.AddRange(GetSemp(negList, "neg", width, height));

            IPerceptron perceptron = new Perceptron(new[] { "pos", "neg" }, width, height, sempls[0].Data.Count);
            ITeacher teacher = new Teacher(perceptron);
            teacher.Teach(sempls, 100);

            using (var open = new SaveFileDialog { Filter = "Файл базы знаний|*.aidb", FilterIndex = 0 })
                if (open.ShowDialog() == DialogResult.OK || open.ShowDialog() == DialogResult.Yes)
                {
                    perceptron.Save(open.FileName);
                }
            /*var recogn = new Recogn();
            recogn.ImgRecogn(new Bitmap("D:\\users\\peter\\Desktop\\..Образцы\\poz\\1.png"), "D:\\users\\peter\\Desktop\\..Образцы\\база.aidb");
            return;*/
        }

        private List<ImageData> GetSemp(IEnumerable<String> path, String @class, Int32 x, Int32 y)
        {
            var sempls = new List<ImageData>();
            Parallel.ForEach(path, (file) =>
            {
                using (var bm = new Bitmap(file))
                {
                    var data = ImgHelpers.ImgPreProcess(bm, x, y);
                    lock (sempls)
                    {
                        sempls.Add(new ImageData
                        {
                            Class = @class,
                            Data = data,
                        });
                    }
                }
            });
            return sempls;
        }

        private void SelectNegPathMethod()
        {
            using (var open = new FolderBrowserDialog { Description = @"Выбор папки с негативными образцами" })
                if (open.ShowDialog() == DialogResult.OK)
                {
                    NegPath = open.SelectedPath;
                }
        }

        private void SelectPosPathMethod()
        {
            using (var open = new FolderBrowserDialog { Description = @"Выбор папки с позитивными образцами" })
                if (open.ShowDialog() == DialogResult.OK)
                {
                    PosPath = open.SelectedPath;
                }
        }

    }
}
