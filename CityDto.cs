using System;

namespace Test
{
    public class CityDto
    {
        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _voivodship;
        public string Voivodship
        {
            get { return _voivodship; }
            set { _voivodship = value; }
        }

        private DateTime _establisedIn;
        public DateTime EstablishedIn
        {
            get { return _establisedIn; }
            set { _establisedIn = value; }
        }

        private string _mayor;
        public string Mayor
        {
            get { return _mayor; }
            set { _mayor = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _population;
        public int Population
        {
            get { return _population; }
            set { _population = value; }
        }
    }
}
