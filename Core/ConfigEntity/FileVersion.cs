using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Core.ConfigEntity
{
    [DataContract]
    public sealed class FileVersion
    {
        [IgnoreDataMember]
        private Int32 _major, _minor, _build, _rev;
        [DataMember]
        public Int32 Major
        {
            get { return _major; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Major));
                _major = value;
            }
        }
        [DataMember]
        public Int32 Minor
        {
            get { return _minor; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Minor));
                _minor = value;
            }
        }
        [DataMember]
        public Int32 Build
        {
            get { return _build; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Build));
                _build = value;
            }
        }
        [DataMember]
        public Int32 Rev
        {
            get { return _rev; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Rev));
                _rev = value;
            }
        }

        public FileVersion()
        {
            Major = Minor = Build = Rev =0;
        }

        public FileVersion(Int32 major = 0, Int32 minor = 0, Int32 build = 0, Int32 rev = 0)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Rev = rev;
        }

        public FileVersion(Version version)
        {
            if (version == null)
                throw new ArgumentNullException(nameof(version));
            Major = version.Major;
            Minor = version.Minor;
            Build = version.Build;
            Rev = version.Revision;
        }

        public FileVersion(String version, Char separator = '.')
        {
            if (String.IsNullOrEmpty(version) || String.IsNullOrWhiteSpace(version))
                throw new ArgumentOutOfRangeException(nameof(version));
            var vmass = version.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
            if (vmass.Length == 0)
                throw new Exception("Incorrect \"version\" string.");
            if (vmass.Length >= 4)
                Rev = vmass[3];
            if (vmass.Length >= 3)
                Build = vmass[2];
            if (vmass.Length >= 2)
                Minor = vmass[1];
            if (vmass.Length >= 1)
                Major = vmass[0];
        }
        /// <summary>
        /// Вернуть строковое представление объекта
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return $"{Major}.{Minor}.{Build}.{Rev}";
        }

        /// <summary>
        /// Оператор не явного преобразования 
        /// </summary>
        /// <param name="ver"></param>
        public static implicit operator FileVersion(Version ver)
        {
            return new FileVersion(ver);
        }
        /// <summary>
        /// Оператор не явного преобразования 
        /// </summary>
        /// <param name="ver"></param>
        public static implicit operator Version(FileVersion ver)
        {
            return new Version(ver._major, ver._minor, ver._build, ver._rev);
        }
        /*
        /// <summary>
        /// Оператор сравнивающий версии на равенство
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        public static Boolean operator ==(FileVersion first, FileVersion last)
        {
            if (ReferenceEquals(last, null) && ReferenceEquals(first, null))
                return true;

            return !ReferenceEquals(first, null)
                   && !ReferenceEquals(last, null)
                   && first.Major == last.Major
                   && first.Minor == last.Minor
                   && first.Build == last.Build;
        }
        /// <summary>
        /// Оператор сравнивающий версии на неравенство
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        public static Boolean operator !=(FileVersion first, FileVersion last)
        {
            return !(first == last);
        }*/
    }
}
