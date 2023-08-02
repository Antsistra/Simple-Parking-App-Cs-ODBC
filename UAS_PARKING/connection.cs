using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAS_PARKING
{
    internal class connection
    {
        OdbcCommand dml;

        public static OdbcDataReader dr;
       public static OdbcConnection koneksi;

        public void BukaKoneksi()
        {
            koneksi = new OdbcConnection("DSN=dbuas");
            koneksi.Open();
        }

    }
}
