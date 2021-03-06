CREATE TABLE "ZAPOSLENI" (
    "MATICNI_BROJ" VARCHAR2(13 BYTE) NOT NULL,
    "IME" VARCHAR2(20 BYTE) NOT NULL,
    "PREZIME" VARCHAR2(20 BYTE) NOT NULL,
    "BROJ_TELEFONA" NUMBER(20,0) NOT NULL,
    "DATUM_RODJENJA" DATE NOT NULL,
    "ADRESA" VARCHAR2(30 BYTE) NOT NULL,
    "FARMACEUT_FLAG" NUMBER(3,0) NOT NULL
);

CREATE UNIQUE INDEX "ZAPOSLENI_PK" ON "ZAPOSLENI"("MATICNI_BROJ");
--=============================================================
CREATE TABLE "FARMACEUT" (
    "MATICNI_BROJ" VARCHAR(13) NOT NULL,
    "DIPLOMIRAO" DATE NOT NULL,
    "OBNOVIO_LICENCU" DATE NOT NULL
);

ALTER TABLE FARMACEUT ADD PRIMARY KEY(MATICNI_BROJ);
ALTER TABLE "FARMACEUT" ADD CONSTRAINT "FARMACEUT_FK" FOREIGN KEY ("MATICNI_BROJ") REFERENCES 
"ZAPOSLENI"("MATICNI_BROJ") ENABLE;
--=============================================================
