CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

CREATE TABLE "Clans" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Clans" PRIMARY KEY AUTOINCREMENT,
    "ClanName" TEXT NULL
);

CREATE TABLE "Podcasts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Podcasts" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL,
    "ClanId" INTEGER NULL,
    CONSTRAINT "FK_Podcasts_Clans_ClanId" FOREIGN KEY ("ClanId") REFERENCES "Clans" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Quotes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Quotes" PRIMARY KEY AUTOINCREMENT,
    "Text" TEXT NULL,
    "PodcastId" INTEGER NOT NULL,
    CONSTRAINT "FK_Quotes_Podcasts_PodcastId" FOREIGN KEY ("PodcastId") REFERENCES "Podcasts" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Quotes_PodcastId" ON "Quotes" ("PodcastId");

CREATE INDEX "IX_Podcasts_ClanId" ON "Podcasts" ("ClanId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200318074438_init', '3.1.2');

