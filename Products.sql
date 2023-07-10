-- Table: public.Products

-- DROP TABLE IF EXISTS public."Products";

CREATE TABLE IF NOT EXISTS public."Products"
(
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    name character varying(1000) COLLATE pg_catalog."default" NOT NULL,
    price numeric NOT NULL,
    description character varying(1000) COLLATE pg_catalog."default",
    type integer NOT NULL,
    delete_at timestamp with time zone,
    CONSTRAINT "PK_Products" PRIMARY KEY (id),
    CONSTRAINT "FK_Products_Product_Types_type" FOREIGN KEY (type)
        REFERENCES public."Product_Types" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Products"
    OWNER to postgres;
-- Index: IX_Products_type

-- DROP INDEX IF EXISTS public."IX_Products_type";

CREATE INDEX IF NOT EXISTS "IX_Products_type"
    ON public."Products" USING btree
    (type ASC NULLS LAST)
    TABLESPACE pg_default;