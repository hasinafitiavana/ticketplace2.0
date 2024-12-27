CREATE TABLE utilisateur (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    prenom VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    mot_de_passe VARCHAR(255) NOT NULL,
    date_de_naissance DATE NOT NULL,
    on_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    on_create TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Type_Place (
    id SERIAL PRIMARY KEY,
    type VARCHAR(50) NOT NULL UNIQUE -- Types could be 'VIP', 'simple', 'invitation', etc.
);

CREATE TABLE Espace (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    adresse VARCHAR(255),
    ville VARCHAR(255),
    code_postal VARCHAR(255),
    capacite INTEGER NOT NULL,
    on_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    on_create TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Evenement (
    id SERIAL PRIMARY KEY,
    espace_id INTEGER NOT NULL REFERENCES Espace(id) ON DELETE CASCADE,
    nom VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    date DATE NOT NULL,
    heure TIME NOT NULL,
    lieu VARCHAR(255) NOT NULL,
    on_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    on_create TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Evenement_Type_Place (
    id SERIAL PRIMARY KEY,
    evenement_id INTEGER NOT NULL REFERENCES Evenement(id) ON DELETE CASCADE,
    type_place_id INTEGER NOT NULL REFERENCES type_place(id) ON DELETE CASCADE,
    nombre_de_places INTEGER NOT NULL,
    prix DECIMAL(10, 2) NOT NULL,
    on_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    on_create TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Place_Vendue (
    id SERIAL PRIMARY KEY,
    evenement_id INTEGER NOT NULL REFERENCES Evenement(id) ON DELETE CASCADE,
    type_place_id INTEGER NOT NULL REFERENCES Type_Place(id) ON DELETE CASCADE,
    utilisateur_id INTEGER NOT NULL REFERENCES Utilisateur(id) ON DELETE CASCADE,
    nombre_de_places INTEGER NOT NULL,
    prix DECIMAL(10, 2) NOT NULL,
    on_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    on_create TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);