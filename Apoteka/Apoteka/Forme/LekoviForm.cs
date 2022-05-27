using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apoteka.Forme
{
    public partial class LekoviForm : Form
    {
        public List<LekPregled> lekoviLista = null;
        public LekoviForm()
        {
            InitializeComponent();
        }
        public int brojLeka = 0;
        private void LekoviForm_Load(object sender, EventArgs e)
        {
            popuniPodacima();

        }

        public void popuniPodacima()
        {
            this.brojLeka = 0;
            this.lekovi.Items.Clear();
            List<LekPregled> listaLeka = DTOManager.vratiSveLekovePregled();


            foreach (LekPregled p in listaLeka)
            {
                ListViewItem item = new ListViewItem(new string[] { p.IdLeka.ToString(), p.KomercijalniNaziv, p.Cena.ToString() });
                this.lekovi.Items.Add(item);
                this.brojLeka++;
            }

            this.lekoviLista = listaLeka;

            tbxBrojLekova.Text = this.brojLeka.ToString();
            this.lekovi.Refresh();
        }

        private void btnPrikaziInfOLeku_Click(object sender, EventArgs e)
        {
            if (lekovi.SelectedItems.Count == 0)
            {
                MessageBox.Show("Izaberite lek koji zelite da obrisete!");
                return;
            }

            //Izvlacimo objeka:
            LekPregled p = this.lekoviLista.ElementAt(this.lekovi.SelectedIndices[0]);

            //Izvlacimo id tipa leka:
            int tipLeka = p.tip;

            //Vrati string tip leka (naziv):
            string nazivTipa = DTOManager.nazivTipaLeka(tipLeka);

            //Vracamo naziv proizvodjaca:
            string proizvodjac = DTOManager.imeProizvodjaca(p.KomercijalniNaziv);

            //Vracamo pakovanje:
            string pakovanje = DTOManager.vratiPakovanje(p.IdLeka);

            //Stampamo na message box:
            string novi = "Id Leka: " + p.IdLeka.ToString() + "\nDoza Odrasli: " + p.DozaOdrasli + "\nDoza Deca: " + p.DozaDeca + "\nDoza trudnice: " + p.DozaTrudnice + "\nDejstvo: " + p.Dejstvo + "\nHemijski Naziv: " + p.HemijskiNaziv + "\nProcenat participacije: " + p.ProcenatParticipacije.ToString() + "\nCena: " + p.Cena.ToString() + "\nKomercijalni naziv: " + p.KomercijalniNaziv + "\nTip: " + nazivTipa + "\nProizvodjac: " + proizvodjac + "\nPakovanje: " + pakovanje;
            MessageBox.Show(novi, "Message", MessageBoxButtons.OK);

        }

        private void btnDodajLek_Click(object sender, EventArgs e)
        {
            LekDodajForm form = new LekDodajForm();
            form.ShowDialog();
            popuniPodacima();
        }

        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            if (lekovi.SelectedItems.Count == 0)
            {
                MessageBox.Show("Izaberite lek koji zelite da obrisete!");
                return;
            }

            int idLeka = Int32.Parse(lekovi.SelectedItems[0].SubItems[0].Text);
            string poruka = "Da li zelite da obrisete izabrani lek?";
            string title = "Pitanje";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(poruka, title, buttons);

            if (result == DialogResult.OK)
            {
                DTOManager.obrisiLekIzSistema(idLeka);
                MessageBox.Show("Brisanje leka je uspesno obavljeno!");
                this.popuniPodacima();
            }
            else
            {

            }
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            //Dodaj proveru da li je izabran neki lek:
            DialogResult dlg = new DialogResult();
            LekIzmeniForma frm = new LekIzmeniForma(this.lekoviLista.ElementAt(this.lekovi.SelectedIndices[0]));
            dlg = frm.ShowDialog();

            if(dlg == DialogResult.OK)
            {
                this.popuniPodacima();
            }
            else
            {
                this.popuniPodacima();
            }
        }

        private void btnPrikaziProdajnaMesta_Click(object sender, EventArgs e)
        {
            LekoviPrikaziProdajnaMesta frm = new LekoviPrikaziProdajnaMesta(DTOManager.mestaProdajeLekova(this.lekoviLista.ElementAt(this.lekovi.SelectedIndices[0]).IdLeka));
            frm.ShowDialog();
        }

        private void btnIndikacije_Click(object sender, EventArgs e)
        {
            LekPrikaziIndikacije frm = new LekPrikaziIndikacije(DTOManager.vratiSveIndikacijeLeka(this.lekoviLista.ElementAt(this.lekovi.SelectedIndices[0]).IdLeka), this.lekoviLista);
            frm.ShowDialog();
        }

        private void btnKontraindikacije_Click(object sender, EventArgs e)
        {
            LekPrikaziKontraindikacije frm = new LekPrikaziKontraindikacije(DTOManager.vratiSveKontraindikacijeLeka(this.lekoviLista.ElementAt(this.lekovi.SelectedIndices[0]).IdLeka), this.lekoviLista);
            frm.ShowDialog();
        }

        private void btnUpakuj_Click(object sender, EventArgs e)
        {
            if(this.lekovi.SelectedItems.Count == 1)
            {
                LekUpakujForm frm = new LekUpakujForm(this.lekoviLista.ElementAt(this.lekovi.SelectedIndices[0]).IdLeka,DTOManager.vratiSvaPakovanja());
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Morate da izaberete samo jedan lek!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
