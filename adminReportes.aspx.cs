﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;

namespace indioSupermercado
{
    public partial class adminReportes : System.Web.UI.Page
    {
        private string stringConnection = usefull.strCon;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSourceBono.ConnectionString = stringConnection;
            SqlDataSourceVendidos.ConnectionString = stringConnection;
            SqlDataSourceFrecuentes.ConnectionString = stringConnection;
            SqlDataSourceExpirados.ConnectionString = stringConnection;
            SqlDataSourceGanancias.ConnectionString = stringConnection;
            GridViewReporteBono.Visible = false;
            //GridViewExpirados.Visible = false

        }

        protected void ButtonReporteBono(object sender, EventArgs e)
        {
            GridViewReporteBono.Visible = true;
            UpdatePanelCliente.Update();
        }

    }
}