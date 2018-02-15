<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="content.aspx.cs" Inherits="sci_nfse.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <title></title>

<head runat="server">
    <link rel="stylesheet" href="styles/styles.css" />
    <script  src="scripts/jquery-1.7.1.js"></script>
</head>
<body>

    <form id="tudo" runat="server">

        <div id="menu_topo">
            
            <div id="infolg">
                <asp:Label ID="n_user" runat="server" Text=""></asp:Label><br />
                <asp:LinkButton ID="logout" runat="server" OnClick="logout_Click">Logout</asp:LinkButton>             
            </div>

        </div>
  
        <div id="menu_lateral">
            <img src="styles/img/começo.png" style="width: 247px" />
            <br />

            <asp:Button ID="btPn1" CssClass="bt_mlateral" runat="server" Text="" OnClick="btPn1_Click" Height="52px" Width="235px" />    
            <asp:Button ID="btPn3" CssClass="bt_mlateral" runat="server" Text="" OnClick="btPn3_Click" Height="52px" Width="235px" />

        </div>

        <div id="content">

            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">

                <asp:View ID="View1" runat="server">
                    <h2>Carregar arquivos</h2>
                    <form id="upload">
                        <asp:FileUpload id="FileUploadControl" runat="server" /><br />
                        <asp:Button runat="server" CssClass="btClass" id="UploadButton" text="&#x21EA; Upload" onclick="UploadButton_Click" />
                        <!--<asp:Label runat="server" id="StatusLabel" text="Upload status: " />-->
                    </form>
                </asp:View>

<asp:View ID="View2" runat="server">
                    <form id="download">
                        <asp:Label runat="server" id="Label1" text="Selecione o arquivo de origem" /><br />
                        <asp:DropDownList ID="sel_notas" runat="server"></asp:DropDownList><br />
                        <asp:Button CssClass="btClass" ID="DownloadButton" runat="server" Text="Download" OnClick="DownloadButton_Click" />
                    </form>
                </asp:View>

                <asp:View ID="View3" runat="server">
                   <h2> Informações da empresa</h2>
                    <form id="infoemp">  

                        <div id="cp1">
                       
                            <asp:Label ID="lb1" runat="server" Text="Natureza da Operação"></asp:Label><br />
                            <asp:TextBox ID="nat_op" runat="server"></asp:TextBox><br />
                        
                        </div>

                        <div id="cp2">

                            <asp:Label ID="lb2" runat="server" Text="Regime Especial de Tributação"></asp:Label><br />
                            <asp:TextBox ID="reg_trib" runat="server"></asp:TextBox><br />
                        
                        </div>
                        
                        <div id="cp3">

                            <asp:Label ID="lb3" runat="server" Text="Optante Simples Nacional"></asp:Label><br />
                            <asp:TextBox ID="opt_nac" runat="server"></asp:TextBox><br />
                        
                        </div>

                        <div id="cp4">

                            <asp:Label ID="lb4" runat="server" Text="Incentivador Cultural"></asp:Label><br />
                            <asp:TextBox ID="inc_cult" runat="server"></asp:TextBox><br />
                        
                        </div>

                        <div id="cp5">

                            <asp:Label ID="lb5" runat="server" Text="Status"></asp:Label><br />
                            <asp:TextBox ID="status" runat="server"></asp:TextBox><br />

                        </div>

                        <div id="cp6">
                        
                            <asp:Label ID="lb6" runat="server" Text="Alíquota"></asp:Label><br />
                            <asp:TextBox ID="aliq" runat="server"></asp:TextBox><br />

                        </div>

                        <div id="cp7">
                        
                            <asp:Label ID="lb7" runat="server" Text="Item da Lista de Serviço"></asp:Label><br />
                            <asp:TextBox ID="lis_serv" runat="server"></asp:TextBox><br />
                        
                        </div>

                        <div id="cp8">

                            <asp:Label ID="lb8" runat="server" Text="Código de Tributação do Município"></asp:Label><br />
                            <asp:TextBox ID="trib_mun" runat="server"></asp:TextBox><br />
                        
                        </div>

                        <div id="cp9">

                            <asp:Label ID="lb9" runat="server" Text="Município de Prestação do Serviço"></asp:Label><br />
                            <asp:TextBox ID="mun_serv" runat="server"></asp:TextBox><br />
                        
                        </div>

                        <div id="cp12">

                            <asp:Label ID="lb12" runat="server" Text="Senha de acesso"></asp:Label><br />
                            <asp:TextBox ID="senha" runat="server"></asp:TextBox><br />
                        
                        </div>

                        <div id="cp11">

                            <asp:Label ID="lb10" runat="server" Text="Formato de Exportacao"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="listaLayout" OnSelectedIndexChanged="listaLayout_SelectedIndexChanged1"></asp:DropDownList>

                        </div>

                        <div id="cp10">

                            <asp:Button CssClass="btClass" ID="btSalva" runat="server" Text="&#10004; Salvar" OnClick="btSalva_Click" />
                            <asp:Button CssClass="btClass" ID="btCancela" runat="server" Text="&#10008; Cancelar" OnClick="btCancela_Click" />
                       
                        </div>

                    </form>
                    
                </asp:View>
                          
            </asp:MultiView>

            <div id="rodape">

                <!--<asp:Label ID="infoST" runat="server" Text="Status : "></asp:Label>-->

            </div> 

        </div>
            
    </form>
</body>
</html>
