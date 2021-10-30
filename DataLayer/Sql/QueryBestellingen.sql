CREATE TABLE [dbo].[Bestellingen] (
    [BestellingId] INT         IDENTITY (1, 1) NOT NULL,
    [KlantId]      INT         NOT NULL,
    [Product]      NVARCHAR (255) NOT NULL,
    [Aantal]       INT         NOT NULL,
    PRIMARY KEY CLUSTERED ([BestellingId] ASC),
    CONSTRAINT [FK_Bestellingen_Klant] FOREIGN KEY ([KlantId]) REFERENCES [dbo].[Klanten] ([KlantId])
);


