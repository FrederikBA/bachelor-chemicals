USE ShwChemicals;

--Product Groups
INSERT INTO [dbo].[ProductGroups] ([GroupName], [Remarks])
VALUES
    ('Olieprodukter', 'Produkter, der anvendes som smøre- og glidemidler f.eks smørefedt og olie. motorolie og skæreolie.'),
    ('Rengørngsmidler', 'Produkter der anvendes til afrensning, affedtning og afkalkning af forskellige overflade. For eksempel maskinopvaskemidler, håndopvaskemidler, kalkfjerner, glasrens med videre.'),
    ('Maling, lak, lim eller fugemasse', 'Produkter, der anvendes til overfladebehandling, sammenføjning (limning) og f.eks. vægmaling, sekundlim, samt produkter der anvendes til fortynding eller forudgående rengøring før maling og limning. Produkterne i denne kategori er kendetegnet ved at de skal være forsynet med et kodenummer – se afsnit D.'),
    ('Brændstof og tilsætningsstoffer til brændstof', 'Produkter, der anvendes som brændstof f.eks. diesel til påhængsmotorer, benzin og produkter, der er beregnet som tilsætning til brændstof.'),
    ('Vandbehandlingskemikalier', 'Produkter, der tilsættes vand f.eks. kedelvand med henblik på at undgå korrosion, justere pH m.v.'),
    ('Andre', 'Her placeres produkter, der ikke falder ind under nogen af de ovenstående kategorier. Det vil typisk være flusmidler, analysekemikalier, trykflasker o.l.')

--Product Status
INSERT INTO [dbo].[ProductStatus] ([StatusName], [Text], [SortOrder])
VALUES
    ('OK', 'Godkendt', 5),
    ('Substitution', 'Godkendt. Bør erstattes med mindre farligt produkt, da det indeholder uønskede stoffer', 6),
    ('ExcludeMissingSDS', 'Er udgået pga. manglede sikkerhedsdatablad', 7),
    ('ExcludeIncompleteData', 'Er udgået pga. manglende data', 8),
    ('ExcludedCancelledByShippingCompanies', 'Er udgået, da det er afemdlt fra rederi/er', 9),
    ('ExcludedCancelledBySupplier', 'Er udgået, da producent/leverandør ikke forhandler produktet mere', 10),
    ('ExcludedReplaced', 'Er udgået fra producent/leverandør og erstattet med produkt nummer XX', 11),
    ('ExcludedForbidden', 'Er forbudt at anvende (bortskaf forsvarligt)', 12),
    ('ExcludedUnknownReason', 'Udgået', 13),
    ('SubstitutionSeen', 'SubstitutionSeen', 14),
    ('ChangeRequestReceived', 'ChangeRequestReceived', 1),
    ('ChangeRequestProcessing', 'ChangeRequestProcessing', 2),
    ('ChangeRequestPending', 'ChangeRequestPending', 3),
    ('ChangeRequestRejected', 'ChangeRequestRejected', 4)

-- Producers (and their addresses)
-- Insert data into ProducerAddress table
INSERT INTO ProducerAddress (Address, City, PostalCode, Country)
VALUES
    ('Strandvejen 52', 'Copenhagen', '2100', 'Denmark'),
    ('Vesterbrogade 26', 'Aarhus', '8000', 'Denmark'),
    ('Oehlenschlægersgade 33', 'Odense', '5000', 'Denmark');

-- Insert data into Producers table
INSERT INTO Producers (CompanyName, ContactPerson, PhoneNumber, AddressId)
VALUES
    ('DanishChem A/S', 'Anders Nielsen', '12345678', 1),
    ('Nordic Chemicals ApS', 'Lise Hansen', '87654321', 2),
    ('Scandinavian Solutions Ltd.', 'Peter Jensen', '33333333', 3);


--Product Categories
-- Inserting categories for 'Olieprodukter' group
INSERT INTO [dbo].[ProductCategories] ([ProductGroupId], [Category])
VALUES
    (1, 'Motorolie'),
    (1, 'Smøremidler'),
    (1, 'Hydraulikolie');

-- Inserting categories for 'Rengørngsmidler' group
INSERT INTO [dbo].[ProductCategories] ([ProductGroupId], [Category])
VALUES
    (2, 'Almindelige rengøringsmidler'),
    (2, 'Desinfektionsmidler'),
    (2, 'Industrielle rengøringsmidler');

-- Inserting categories for 'Maling, lak, lim eller fugemasse' group
INSERT INTO [dbo].[ProductCategories] ([ProductGroupId], [Category])
VALUES
    (3, 'Industriel maling'),
    (3, 'Lak'),
    (3, 'Lim');

-- Inserting categories for 'Brændstof og tilsætningsstoffer til brændstof' group
INSERT INTO [dbo].[ProductCategories] ([ProductGroupId], [Category])
VALUES
    (4, 'Benzin'),
    (4, 'Diesel'),
    (4, 'Tilsætningsstoffer til brændstof');

-- Inserting categories for 'Vandbehandlingskemikalier' group
INSERT INTO [dbo].[ProductCategories] ([ProductGroupId], [Category])
VALUES
    (5, 'Filtreringsmidler'),
    (5, 'Desinfektionsmidler til vand'),
    (5, 'pH-reguleringsmidler');

-- Inserting categories for 'Andre' group
INSERT INTO [dbo].[ProductCategories] ([ProductGroupId], [Category])
VALUES
    (6, 'Specialkemikalier'),
    (6, 'Industrikemikalier'),
    (6, 'Husholdningskemikalier');

-- Some products
-- Inserting five chemical products into the Products table
INSERT INTO Products (Name, ProductCategoryId, ProducerId, ProductStatusId)
VALUES
    ('Saltsyre', 17, 1, 1),
    ('Natriumhydroxid', 17, 2, 1),
    ('Eddikesyre', 17, 3, 1),
    ('Ammoniumnitrat', 12, 1, 1),
    ('Ethanol', 12, 3, 1);