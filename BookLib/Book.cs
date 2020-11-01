using System;
using System.Collections.Generic;
using Windows.System.Linq;
using Windows.ApplicationModel.Wallet.System.Text;
using Windows.System.Threading.Tasks;

namespace BookLib
{
    public class Book : AbstractItem
    {

        protected int _edition;//usually number- edition 1 or edition 2018
        protected string _author;
        protected int _numPart;
        protected List<Genre> _genres;

        public Book(string name, int copyNum, string publisher, double priceOfRent, int edition, string author, int numPart, List<Genre> genres) : base(name, copyNum, publisher, priceOfRent)
        {
            _edition = edition;
            _author = author;
            _numPart = numPart;
            _genres = genres;
        }

        //properties
        public int Edition
        {
            get { return _edition; }
            set { _edition = value; }
        }
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
            }
        }
        public int NumPart
        {
            get { return _numPart; }
            set { _numPart = value; }
        }
        public List<Genre> Geners
        {
            get { return _genres; }
            set
            {
                _genres = value;
            }
        }

        public override string ToString()
        {
            StringBuilder descrpition = new StringBuilder();
            descrpition.Append(base.ToString());
            descrpition.Append(" Edition:").Append(_edition);
            descrpition.Append(" Author:").Append(_author);
            descrpition.Append(" Part:").Append(_numPart);
            descrpition.Append(" Genres:");

            for (int i = 0; i < Geners.Count; i++)
            {
                descrpition.Append(Geners[i]).Append(",");
            }
            return descrpition.ToString();
        }

    }
}
