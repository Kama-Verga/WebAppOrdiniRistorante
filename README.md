**Scopo del Applicativo**

Il progetto comprende un programma per la gestione di ordini per un ristorante, prendendo in considerazioni le API per la: 
- Creazione di un utente di tipo Cliente (anonima senza autenticazione)login
- sistema di Autenticazione/login tramite JWT
- Creazione di un ordine. creato l'ordine che restituisce il numero d'ordine e il prezzo totale, con un 10% di sconto se si prende 1 pacchetto composto da 1 primo 1 secondo 1 contorno ed 1 dolce.
- visualizzazione di uno Storico degli ordini, che cambia in base al ruolo dell'utente.
   gli utenti di tipo Cliente, visualizza l'elenco degli ordini da lui effettuato.
   gli utenti di tipo Amministratore, possono visualizzare l'elenco di tutti gli ordini effettuati o tutti gli ordini effettuat da uno specifico utente, (tramite un range di date ed un ID)

---

**Per inizializzare il progetto.**
- il progetto sfrutta un Database hostato in macchina locale Già esistente, suggerisco l'utilizzo di SSMS per la creazione del DB ( https://learn.microsoft.com/it-it/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
  e SQL Server su Azure per l'host in locale (https://www.microsoft.com/it-it/sql-server/sql-server-downloads)
- una volta avviato il server tramite SQL Server Azure, si esegue il file "Genera DB.sql" su SSMS che creerà tutte le tabelle e compilerà la tabella del menù.
- ora sempre su SSMS possiamo eseguire il file "visualizza tutte le tabelle.sql" e vedere se le tabelle sono state create correttamente, dovrebbero essere visibili le tabelle (Utente, Ordine, Prodotto, ProdottoInOrdine).
- ora bisogna sostituire il link di connessione al interno del progetto  ![1_aM6LRM301UyXFh7APAZYxg](https://github.com/user-attachments/assets/86a92f60-6e5d-4d01-950f-65602042a439)
  qui troviamo il link di connessione e lo andiamo ha mettere al interno del del MyDBContent, ora il progetto è collegato correttamento al DB ora non resta che avviare ed utilizzare il le API.
  
  ![MyDBContent](https://github.com/user-attachments/assets/925f30ab-9f6d-4136-a04f-e44434d0f814) ![Connection Link](https://github.com/user-attachments/assets/2ebd4c86-d638-4634-a6ba-0c3a7e0c27c6)

---

**Utilizzare Le API Tramite Swagger.**

una volta compilato il progetto la schermata dello Swagger si presenterà cosi, ![image](https://github.com/user-attachments/assets/c64bb22c-3301-441e-b7c8-d7a7d1563273)
- crea Utente, crea un utente con email, nome, cognome, password come stringhe, e ruolo dove 0 è l'admin e qualunque altro numero un utente comune (questa scelta per il poter introdurre nuovi ruoli nel futuro o sostituire il sistema con un enumeratore in un possibile futuro)
- Create Token, preso in input la email e la password di un utente restituisce un token tra virgolette come
***"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6InN0cmluZyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InN0cmluZyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IjAiLCJleHAiOjE3MzM4NTQ0NjEsImlzcyI6ImlsLXR1by1pc3N1ZXIifQ.21nB9ArHKjlYxI94XGNsityQ4fxM6K5Da8iIsz_4lCk"***
- questo token va inserito nell tasto authorize cosi che da ora in poi si sia loggati in questo modo si puo dare la paternità ha qualcuno hai prossimi compiti svolti dal API

![authorizze](https://github.com/user-attachments/assets/e3a07ef1-ac6d-4b53-85b6-5aa9b4c80614)
- Menù, mostra l'elenco dei cibi aggiungibili ad un ordine con il loro relativo ID
- CreaOrdine, prende in input l'indirizzo di consegna e un elenco di prodotti che vanno dal 1 al 12, il sistema ritorna il prezzo applicando correttamente gli eventuali sconti.
- VisualizzaOrdini, il metodo più complesso, infatti si comporta diversamente se l'utente è un admin o no, nel caso non sia un admin manda la cronologia dei suoi ordini nel range delle due date messe, nel caso in cui l'utente sia un admin vedrà di base tutti gli ordini di tutti gli utenti in un range, ce un ID opsionale che può essere messo solo se l'utente è un admin, in questo caso si vedranno tutti gli ordini del utente corrispondente al ID.
