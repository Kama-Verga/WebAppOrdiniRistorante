
CREATE TABLE Utente (
    Id INT IDENTITY(1,1) PRIMARY KEY,          -- Chiave primaria con auto-incremento
    Mail VARCHAR(255) NOT NULL,
    Nome VARCHAR(100) NOT NULL,                -- Nome dell'utente
    Cognome VARCHAR(100) NOT NULL,             -- Cognome dell'utente
    Password VARCHAR(255) NOT NULL,            -- Password dell'utente (considera di usare un hash)
    Ruolo INT NOT NULL                         -- Ruolo dell'utente (0 per utente normale, 1 per amministratore, ecc.)
);


CREATE TABLE Prodotto (
    Id INT IDENTITY(1,1) PRIMARY KEY,          -- Chiave primaria con auto-incremento
    Nome VARCHAR(100) NOT NULL,               -- Nome del prodotto
    Prezzo DECIMAL(18, 2) NOT NULL,           -- Prezzo con precisione e scala
    Tipo INT NOT NULL                          -- Tipo di prodotto (0 primo, 1 secondo, ecc.)
);


INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Pasta alla Bolognese', 8.99, 0);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Pasta Carbonara', 10.50, 0);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Tagliatelle ai funghi porcini', 9.99, 0);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Bistecca alla fiorentina', 14.99, 1);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Pollo alla cacciatora', 10.99, 1);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Branzino al forno con patate', 14.99, 1);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Patate al forno', 2.99, 2);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Insalata mista', 2.99, 2);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Zucchine grigliate', 2.99, 2);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Tiramisù', 4.99, 3);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Panna cotta ai frutti di bosco', 3.50, 3);
INSERT INTO Prodotto (Nome, Prezzo, Tipo) VALUES ('Cheesecake al limone', 4.00, 3);
INSERT INTO Utente (Mail, Nome, Cognome, Password, Ruolo) VALUES ('string', 'string', 'string', 'string',0);


CREATE TABLE Ordine (
    Numero_Ordine INT IDENTITY(1,1) PRIMARY KEY,  -- ID auto-incrementale
    Data_creazione DATETIME NOT NULL,             -- Data di creazione
    Indirizzo_Di_Consegna NVARCHAR(500) NOT NULL, -- Indirizzo di consegna
    Prezzo DECIMAL(10, 2) NOT NULL,                -- Prezzo totale dell'ordine
	UtenteId INT NOT NULL
);



CREATE TABLE ProdottoInOrdine (
    Id INT IDENTITY(1,1) PRIMARY KEY,             -- Aggiungi un campo Id auto-incrementale
    OrdineId INT,                                 -- Riferimento all'ordine
    ProdottoId INT,                               -- Riferimento al prodotto
    FOREIGN KEY (OrdineId) REFERENCES Ordine(Numero_Ordine) ON DELETE CASCADE,  -- Associazione con la tabella Ordine
    FOREIGN KEY (ProdottoId) REFERENCES Prodotto(Id) ON DELETE CASCADE -- Associazione con la tabella Prodotto
);

