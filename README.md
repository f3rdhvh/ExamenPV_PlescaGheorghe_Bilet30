# Examen la materia Programarea Vizuala
## Realizat de Plesca Gheorghe ( biletul nr. 30 )

Scurt si la obiect (scris rapid, fara diacritice):

Aplicatia e un mic proiect WinForms care simuleaza inregistrarea comenzilor pentru o cafenea si salvarea lor intr-o baza SQL Server.

Ce face aplicatia (pe scurt):
- Form1: interfața principala unde completezi nr. comanda, data, adresa si alegi felurile si deserturile.
- Buton "Afișeaza comanda": valideaza numar si adresa, arata comanda in listbox si insereaza in tabela comenzi din baza de date.
- Buton "Sterge afișarea": sterge inregistrarea din DB dupa nr. comanda (daca exista) si curata listbox-ul.
- Buton "Istoric comenzi": deschide o fereastra noua (modal) care afiseaza toate comenzile intr-un DataGridView read-only. Fereastra are inaltime maxima setata ~300px si DataGridView are scroll daca sunt prea multe inregistrari.
- Buton "Creaza Baza de Date": incearca sa creeze baza 'bioprinz' si tabela 'comenzi'.

Detalii tehnice (putin mai complex):
- Proiect: WinForms (.NET Framework 4.7.2)
- Clasa DatabaseManager gestioneaza conexiunea si operatiile SQL: creaza baza, insereaza comanda, sterge comanda, interogheaza toate comenzile.
- Baza/tabela: database 'bioprinz', tabela 'comenzi' (id INT IDENTITY, nrcomanda, datacomanda, adresaclient, felintai, feldoi, desert).
- Conexiune: string-ul implicit din DatabaseManager.cs este: Data Source=DESKTOP-C24JO84;Initial Catalog=master;Integrated Security=True
  - Schimba "Data Source" la instanta ta SQL Server (ex: localhost\SQLEXPRESS) inainte de a crea/folosi baza.

Rulare rapida:
1. Deschide solutia in Visual Studio (folosesc VS 2026, dar ar trebui sa mearga si alte versiuni care suporta .NET 4.7.2).
2. Modifica connection string in DatabaseManager.cs daca e nevoie.
3. Ruleaza aplicatia (F5).
4. Daca nu ai baza, apasa "Creaza Baza de Date".
5. Completeaza Nr. comanda si Adresa (sunt obligatorii), alege optiunile si apasa "Afișează comanda".
6. Daca vrei sa vezi toate comenzile, apasa "Istoric comenzi" -> apare o fereastra cu DataGridView si scroll.

Validari si comportament important:
- Nr comanda si Adresa sunt obligatorii la inserare.
- Insert/sterge/interogare sunt incapsulate in try/catch si afiseaza mesaje de eroare in MessageBox daca ceva merge prost.
- Butonul Istoric deschide form modal; daca vrei sa fie non-modal sau "deasupra" schimb asta usor in cod.

Probleme cunoscute / sugestii:
- Daca baza exista, "Creaza Baza de Date" va arunca exceptie (mesaj afisat). E normal, poti ignora sau sterge DB manual si reface.
- Connection string trebuie corectat pentru mediul tau.
- Nu sunt teste automate incluse.

Daca vrei sa modific ceva rapid (eu am scris asta pe fuga):
- schimb numele sau aspectul coloanelor afisate in FormIstoric,
- fac fereastra istoricului non-modal si mereu "on-top",
- adaug filtrare/ cautare si paginare in DataGridView,
- schimbi layoutul UI pentru ecrane mici.

Mersi, asta e tot.
