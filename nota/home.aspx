<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="nota.home" %>

<!DOCTYPE html>
    

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    
    <link href="styles/login.css" rel="stylesheet" /> 
    <meta http-equiv="Content-Type" content="text/html" charset="UTF-8"/>
    <script src="js/index.js"></script>
    <title>Login </title>
    <link href="https://fonts.googleapis.com/css?family=Ubuntu" rel="stylesheet"/>
</head>
<body>
    <!-- TELA DE LOGIN --> 
    <form id="form1" runat="server">
        
        <div id="tudo">

            <div class="wrap">

                <h1>
                    <center>LOGIN<img src="img/secreto.png" /></center>
                </h1>
                
                <div>
                    <div style="background: white;">
                         <!--  textbox para usuario  -->                         
                        <span class="input-group-addon" style="background: white;">&#128100</span>
                        <asp:TextBox ID="usuario" runat="server" placeholder=" Usuário" required>
                        </asp:TextBox>
                    </div>
                    <div class="bar">
                        <i></i>
                        <br />
                    </div>
                    <div style="background: white;">
                        <!--  textbox para senha  --> 
                        <span class="input-group-addon" style="background: white;">&#128274</span>
                        <asp:TextBox ID="senha" runat="server" placeholder=" Senha" required TextMode="Password">
                        </asp:TextBox>
                        
                    </div>
                    <div></div>
                    <br />
                        <!--  Esqueceu sua senha  --> 
                    <center><a href="" class="forgot_link">Esqueceu sua senha?</a></center>
                </div>
                <br />
                <div id="info">
        
                <asp:Label ID="infolg" runat="server" Text=""></asp:Label><br />
  
            </div>

                <center>

              <asp:Button ID="Button1" runat="server" Text="Acessar" OnClick="Button1_Click"></asp:Button>  
          
            </center>

            </div>

        </div>
        
    </form>

</body>
</html>
