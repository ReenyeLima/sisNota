<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="nota.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Nota fiscal eletronica</title>
    <link rel="stylesheet" href="styles/StyleSheet1.css" />
    <link href="https://fonts.googleapis.com/css?family=Abril+Fatface|Anton" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/JavaScript.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Josefin+Sans|PT+Sans+Narrow" rel="stylesheet" />

</head>
<body>

    <form id="tudo" runat="server">
        <div id="nav">
            <ul>

                <li><span class="enviar"></span><a href="#" runat="server" onserverclick="Unnamed_ServerClick">Enviar RPS </a></li>
                <li><span class="info"></span><a href="#" runat="server" onserverclick="Unnamed_ServerClick1">Informações da empresa </a></li>
                <li><span class="consulta"></span><a href="#" runat="server" onserverclick="Unnamed_ServerClick2">Consulta</a></li>

            </ul>


        </div>

        <div id="view">
            <asp:MultiView ID="MultiView2" runat="server" OnActiveViewChanged="MultiView2_ActiveViewChanged">
                <asp:View ID="View1" runat="server">

                    <div id="enviararq">


                        <h1>Enviar arquivos</h1>
                        <img src="img/up2.png" /></><br />

                        <div class="upload-btn-wrapper">
                            <button class="btn">Escolha um arquivo</button>
                            <asp:FileUpload type="file" ID="myfile" name="myfile" runat="server" /><br />
                        </div>
                        <br />
                        <br />

                        <asp:Button ID="upbutton" OnClick="upbutton_Click" runat="server" Text="&#x21EA; Enviar" />
                        <asp:Table ID="Table1" runat="server"></asp:Table>

                    </div>

                </asp:View>


                <asp:View ID="View2" runat="server">
                    <div id="info">


                        <h2>Informações da empresa
                            <img src="img/infoblue2.png" /></></h2>
                        <br />

                        <form id="infoemp">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Natureza da Operação"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Regime Especial de Tributação"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Optante Simples Nacional"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Incentivador Cultural"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Status"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Alíquota"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Item da Lista de Serviço"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Código de Tributação do Município"></asp:Label><br />
                                    </td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Município de Prestação do Serviço"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Senha de acesso"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lb10" runat="server" Text="Formato de Exportação"></asp:Label><br />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="listaLayout"></asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button CssClass="btClass" ID="btSalva" runat="server" Text="&#10004; Salvar" />
                                    </td>
                                    <td>
                                        <asp:Button CssClass="btClass" ID="btCancela" runat="server" Text="&#10006; Cancelar" /></>
                                    
                                    </td>
                                </tr>

                            </table>
                        </form>
                    </div>
                </asp:View>

                <asp:View ID="View3" runat="server">
                    <div id="consulta">
                        <center>     
                    <h1> Consultas  <img src="img/consultag.png" /> </h1>
                   </center>

                        <div id="situacaolote">
                            <h3>Situação do lote</h3>
                            <br />
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Prestador: "></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                            </td>
                            <br />
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Protocolo:"></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                            </td>
                            <asp:Button ID="btConsultaLote" OnClick="btConsultaLote_Click" runat="server" Text="&#x1f50d;Consultar" />
                        </div>

                        <div id="nfsporrps">
                            <h3>NFS-e por RPS</h3>
                            <br />
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="Identificação do RPS: "></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                            </td>
                            <br />
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Prestador:"></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                            </td>
                            <asp:Button ID="btConsultaNfseRps" runat="server" Text="&#x1f50d;Consultar" />
                        </div>
                        <div id="loterps">
                            <h3>Lote de RPS</h3>
                            <br>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Prestador: "></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                            </td>
                            <br />
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="Protocolo:"></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                            </td>
                            <asp:Button ID="btConsultaloteRPS" runat="server" Text="&#x1f50d;Consultar" />
                        </div>
                        <div id="nfse">
                            <h3>NFS-e </h3>
                            <br />
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="Prestador: "></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                            </td>
                            <br />
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Número NFS-e:"></asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                            </td>
                            <asp:Button ID="btConsultaNfse" runat="server" Text="&#x1f50d;Consultar" />

                        </div>
                    </div>
                </asp:View>

            </asp:MultiView>

        </div>

    </form>

</body>
</html>
