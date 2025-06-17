USE AcademicDb;
GO

-- 1) Journals
INSERT INTO Journals (Name, Issn, Publisher)
VALUES
  ('Journal A', '1234-5678', 'PubCo'),
  ('Journal B', '9876-5432', 'ResearchCo');

-- 2) Authors
INSERT INTO Authors (FirstName, LastName, Affiliation)
VALUES
  ('Alice', 'Smith', 'Uni X'),
  ('Bob',   'Jones', 'Uni Y'),
  ('Carol', 'Wang',  'Institute Z');

-- 3) Articles
INSERT INTO Articles (Title, Abstract, PublicationDate, JournalId)
VALUES
  ('Study of Foo',   'An in-depth look at foo.',      '2025-01-15', 1),
  ('Bar Dynamics',   'Exploring the bar phenomenon.', '2025-02-20', 2);

-- 4) Article-Author relationships
INSERT INTO ArticleAuthors (ArticleId, AuthorId)
VALUES
  (1, 1),  -- Alice ↔ Study of Foo
  (1, 2),  -- Bob   ↔ Study of Foo
  (2, 2),  -- Bob   ↔ Bar Dynamics
  (2, 3);  -- Carol ↔ Bar Dynamics
