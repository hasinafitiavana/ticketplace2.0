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