--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2024-10-01 11:39:36

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 4925 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 229 (class 1259 OID 82966)
-- Name: Benefits; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Benefits" (
    "Name" character varying(255),
    "Description" character varying(255),
    "Discount" integer,
    "Id" integer NOT NULL
);


ALTER TABLE public."Benefits" OWNER TO postgres;

--
-- TOC entry 239 (class 1259 OID 83002)
-- Name: Benefits_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Benefits_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Benefits_Id_seq" OWNER TO postgres;

--
-- TOC entry 4926 (class 0 OID 0)
-- Dependencies: 239
-- Name: Benefits_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Benefits_Id_seq" OWNED BY public."Benefits"."Id";


--
-- TOC entry 223 (class 1259 OID 82903)
-- Name: Customers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Customers" (
    "Id" integer NOT NULL,
    "First_Name" character varying(20),
    "Last_Name" character varying(20),
    "Phone" character varying(20),
    "Address" character varying(50),
    "Birth_Date" date
);


ALTER TABLE public."Customers" OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 82902)
-- Name: Customers_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Customers_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Customers_Id_seq" OWNER TO postgres;

--
-- TOC entry 4927 (class 0 OID 0)
-- Dependencies: 222
-- Name: Customers_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Customers_Id_seq" OWNED BY public."Customers"."Id";


--
-- TOC entry 228 (class 1259 OID 82920)
-- Name: Delivery; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Delivery" (
    "Id" integer NOT NULL,
    "Order_Id" integer,
    "Delivery_Date" date,
    "Status" character varying(50)
);


ALTER TABLE public."Delivery" OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 82919)
-- Name: Delivery_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Delivery_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Delivery_Id_seq" OWNER TO postgres;

--
-- TOC entry 4928 (class 0 OID 0)
-- Dependencies: 227
-- Name: Delivery_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Delivery_Id_seq" OWNED BY public."Delivery"."Id";


--
-- TOC entry 220 (class 1259 OID 82893)
-- Name: Indications; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Indications" (
    "Id" integer NOT NULL,
    "Name" character varying(255)
);


ALTER TABLE public."Indications" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 82892)
-- Name: Indications_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Indications_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Indications_Id_seq" OWNER TO postgres;

--
-- TOC entry 4929 (class 0 OID 0)
-- Dependencies: 219
-- Name: Indications_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Indications_Id_seq" OWNED BY public."Indications"."Id";


--
-- TOC entry 218 (class 1259 OID 82886)
-- Name: Medicines; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Medicines" (
    "Id" integer NOT NULL,
    "Name" character varying(255)
);


ALTER TABLE public."Medicines" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 82885)
-- Name: Medicines_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Medicines_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Medicines_Id_seq" OWNER TO postgres;

--
-- TOC entry 4930 (class 0 OID 0)
-- Dependencies: 217
-- Name: Medicines_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Medicines_Id_seq" OWNED BY public."Medicines"."Id";


--
-- TOC entry 221 (class 1259 OID 82899)
-- Name: Medicines_Indications_Map; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Medicines_Indications_Map" (
    "Medicines_Id" integer NOT NULL,
    "Indications_Id" integer
);


ALTER TABLE public."Medicines_Indications_Map" OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 82916)
-- Name: Order_Details; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Order_Details" (
    "Order_Id" integer NOT NULL,
    "Medicine_Id" integer,
    "Quantity" integer
);


ALTER TABLE public."Order_Details" OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 82973)
-- Name: Order_Statuses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Order_Statuses" (
    "Id" integer NOT NULL,
    "Name" character varying(255)
);


ALTER TABLE public."Order_Statuses" OWNER TO postgres;

--
-- TOC entry 240 (class 1259 OID 83012)
-- Name: Order_Statuses_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Order_Statuses_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Order_Statuses_Id_seq" OWNER TO postgres;

--
-- TOC entry 4931 (class 0 OID 0)
-- Dependencies: 240
-- Name: Order_Statuses_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Order_Statuses_Id_seq" OWNED BY public."Order_Statuses"."Id";


--
-- TOC entry 225 (class 1259 OID 82910)
-- Name: Orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Orders" (
    "Id" integer NOT NULL,
    "Customer_Id" integer,
    "Order_Date" date,
    "Status_Id" integer,
    "Benefit_Id" integer
);


ALTER TABLE public."Orders" OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 82909)
-- Name: Orders_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Orders_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Orders_Id_seq" OWNER TO postgres;

--
-- TOC entry 4932 (class 0 OID 0)
-- Dependencies: 224
-- Name: Orders_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Orders_Id_seq" OWNED BY public."Orders"."Id";


--
-- TOC entry 232 (class 1259 OID 82978)
-- Name: Price_History; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Price_History" (
    "Id" integer NOT NULL,
    "Medecine_Id" integer,
    "Price" numeric(10,2),
    "Date" date
);


ALTER TABLE public."Price_History" OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 82977)
-- Name: Price_History_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Price_History_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Price_History_Id_seq" OWNER TO postgres;

--
-- TOC entry 4933 (class 0 OID 0)
-- Dependencies: 231
-- Name: Price_History_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Price_History_Id_seq" OWNED BY public."Price_History"."Id";


--
-- TOC entry 216 (class 1259 OID 82877)
-- Name: Suppliers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Suppliers" (
    "Id" integer NOT NULL,
    "Name" character varying(255),
    "Contact_Info" character varying(255)
);


ALTER TABLE public."Suppliers" OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 82876)
-- Name: Suppliers_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Suppliers_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Suppliers_Id_seq" OWNER TO postgres;

--
-- TOC entry 4934 (class 0 OID 0)
-- Dependencies: 215
-- Name: Suppliers_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Suppliers_Id_seq" OWNED BY public."Suppliers"."Id";


--
-- TOC entry 234 (class 1259 OID 82983)
-- Name: Supplies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Supplies" (
    "Id" integer NOT NULL,
    "Supplier_Id" integer,
    "Supply_Date" date
);


ALTER TABLE public."Supplies" OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 82982)
-- Name: Supplies_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Supplies_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Supplies_Id_seq" OWNER TO postgres;

--
-- TOC entry 4935 (class 0 OID 0)
-- Dependencies: 233
-- Name: Supplies_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Supplies_Id_seq" OWNED BY public."Supplies"."Id";


--
-- TOC entry 238 (class 1259 OID 82993)
-- Name: Supply_Usage; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Supply_Usage" (
    "Id" integer NOT NULL,
    "Supply_Item_Id" integer,
    "Order_Detail_id" integer
);


ALTER TABLE public."Supply_Usage" OWNER TO postgres;

--
-- TOC entry 237 (class 1259 OID 82992)
-- Name: Supplu_Usage_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Supplu_Usage_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Supplu_Usage_Id_seq" OWNER TO postgres;

--
-- TOC entry 4936 (class 0 OID 0)
-- Dependencies: 237
-- Name: Supplu_Usage_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Supplu_Usage_Id_seq" OWNED BY public."Supply_Usage"."Id";


--
-- TOC entry 236 (class 1259 OID 82988)
-- Name: Supply_Items; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Supply_Items" (
    "Id" integer NOT NULL,
    "Supply_Id" integer,
    "Medicine_Id" integer,
    "Quantity" integer,
    "Purchase_Price" numeric(10,2),
    "Expiry_Date" date
);


ALTER TABLE public."Supply_Items" OWNER TO postgres;

--
-- TOC entry 235 (class 1259 OID 82987)
-- Name: Supply_Items_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Supply_Items_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Supply_Items_Id_seq" OWNER TO postgres;

--
-- TOC entry 4937 (class 0 OID 0)
-- Dependencies: 235
-- Name: Supply_Items_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Supply_Items_Id_seq" OWNED BY public."Supply_Items"."Id";


--
-- TOC entry 4703 (class 2604 OID 83003)
-- Name: Benefits Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Benefits" ALTER COLUMN "Id" SET DEFAULT nextval('public."Benefits_Id_seq"'::regclass);


--
-- TOC entry 4700 (class 2604 OID 82906)
-- Name: Customers Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Customers" ALTER COLUMN "Id" SET DEFAULT nextval('public."Customers_Id_seq"'::regclass);


--
-- TOC entry 4702 (class 2604 OID 82923)
-- Name: Delivery Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Delivery" ALTER COLUMN "Id" SET DEFAULT nextval('public."Delivery_Id_seq"'::regclass);


--
-- TOC entry 4699 (class 2604 OID 82896)
-- Name: Indications Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Indications" ALTER COLUMN "Id" SET DEFAULT nextval('public."Indications_Id_seq"'::regclass);


--
-- TOC entry 4698 (class 2604 OID 82889)
-- Name: Medicines Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Medicines" ALTER COLUMN "Id" SET DEFAULT nextval('public."Medicines_Id_seq"'::regclass);


--
-- TOC entry 4704 (class 2604 OID 83013)
-- Name: Order_Statuses Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Order_Statuses" ALTER COLUMN "Id" SET DEFAULT nextval('public."Order_Statuses_Id_seq"'::regclass);


--
-- TOC entry 4701 (class 2604 OID 82913)
-- Name: Orders Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders" ALTER COLUMN "Id" SET DEFAULT nextval('public."Orders_Id_seq"'::regclass);


--
-- TOC entry 4705 (class 2604 OID 82981)
-- Name: Price_History Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Price_History" ALTER COLUMN "Id" SET DEFAULT nextval('public."Price_History_Id_seq"'::regclass);


--
-- TOC entry 4697 (class 2604 OID 82880)
-- Name: Suppliers Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Suppliers" ALTER COLUMN "Id" SET DEFAULT nextval('public."Suppliers_Id_seq"'::regclass);


--
-- TOC entry 4706 (class 2604 OID 82986)
-- Name: Supplies Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supplies" ALTER COLUMN "Id" SET DEFAULT nextval('public."Supplies_Id_seq"'::regclass);


--
-- TOC entry 4707 (class 2604 OID 82991)
-- Name: Supply_Items Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supply_Items" ALTER COLUMN "Id" SET DEFAULT nextval('public."Supply_Items_Id_seq"'::regclass);


--
-- TOC entry 4708 (class 2604 OID 82996)
-- Name: Supply_Usage Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supply_Usage" ALTER COLUMN "Id" SET DEFAULT nextval('public."Supplu_Usage_Id_seq"'::regclass);


--
-- TOC entry 4908 (class 0 OID 82966)
-- Dependencies: 229
-- Data for Name: Benefits; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Benefits" ("Name", "Description", "Discount", "Id") FROM stdin;
\.


--
-- TOC entry 4902 (class 0 OID 82903)
-- Dependencies: 223
-- Data for Name: Customers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Customers" ("Id", "First_Name", "Last_Name", "Phone", "Address", "Birth_Date") FROM stdin;
\.


--
-- TOC entry 4907 (class 0 OID 82920)
-- Dependencies: 228
-- Data for Name: Delivery; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Delivery" ("Id", "Order_Id", "Delivery_Date", "Status") FROM stdin;
\.


--
-- TOC entry 4899 (class 0 OID 82893)
-- Dependencies: 220
-- Data for Name: Indications; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Indications" ("Id", "Name") FROM stdin;
\.


--
-- TOC entry 4897 (class 0 OID 82886)
-- Dependencies: 218
-- Data for Name: Medicines; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Medicines" ("Id", "Name") FROM stdin;
\.


--
-- TOC entry 4900 (class 0 OID 82899)
-- Dependencies: 221
-- Data for Name: Medicines_Indications_Map; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Medicines_Indications_Map" ("Medicines_Id", "Indications_Id") FROM stdin;
\.


--
-- TOC entry 4905 (class 0 OID 82916)
-- Dependencies: 226
-- Data for Name: Order_Details; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Order_Details" ("Order_Id", "Medicine_Id", "Quantity") FROM stdin;
\.


--
-- TOC entry 4909 (class 0 OID 82973)
-- Dependencies: 230
-- Data for Name: Order_Statuses; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Order_Statuses" ("Id", "Name") FROM stdin;
\.


--
-- TOC entry 4904 (class 0 OID 82910)
-- Dependencies: 225
-- Data for Name: Orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Orders" ("Id", "Customer_Id", "Order_Date", "Status_Id", "Benefit_Id") FROM stdin;
\.


--
-- TOC entry 4911 (class 0 OID 82978)
-- Dependencies: 232
-- Data for Name: Price_History; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Price_History" ("Id", "Medecine_Id", "Price", "Date") FROM stdin;
\.


--
-- TOC entry 4895 (class 0 OID 82877)
-- Dependencies: 216
-- Data for Name: Suppliers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Suppliers" ("Id", "Name", "Contact_Info") FROM stdin;
\.


--
-- TOC entry 4913 (class 0 OID 82983)
-- Dependencies: 234
-- Data for Name: Supplies; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Supplies" ("Id", "Supplier_Id", "Supply_Date") FROM stdin;
\.


--
-- TOC entry 4915 (class 0 OID 82988)
-- Dependencies: 236
-- Data for Name: Supply_Items; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Supply_Items" ("Id", "Supply_Id", "Medicine_Id", "Quantity", "Purchase_Price", "Expiry_Date") FROM stdin;
\.


--
-- TOC entry 4917 (class 0 OID 82993)
-- Dependencies: 238
-- Data for Name: Supply_Usage; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Supply_Usage" ("Id", "Supply_Item_Id", "Order_Detail_id") FROM stdin;
\.


--
-- TOC entry 4938 (class 0 OID 0)
-- Dependencies: 239
-- Name: Benefits_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Benefits_Id_seq"', 1, false);


--
-- TOC entry 4939 (class 0 OID 0)
-- Dependencies: 222
-- Name: Customers_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Customers_Id_seq"', 1, false);


--
-- TOC entry 4940 (class 0 OID 0)
-- Dependencies: 227
-- Name: Delivery_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Delivery_Id_seq"', 1, false);


--
-- TOC entry 4941 (class 0 OID 0)
-- Dependencies: 219
-- Name: Indications_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Indications_Id_seq"', 1, false);


--
-- TOC entry 4942 (class 0 OID 0)
-- Dependencies: 217
-- Name: Medicines_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Medicines_Id_seq"', 1, false);


--
-- TOC entry 4943 (class 0 OID 0)
-- Dependencies: 240
-- Name: Order_Statuses_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Order_Statuses_Id_seq"', 1, false);


--
-- TOC entry 4944 (class 0 OID 0)
-- Dependencies: 224
-- Name: Orders_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Orders_Id_seq"', 1, false);


--
-- TOC entry 4945 (class 0 OID 0)
-- Dependencies: 231
-- Name: Price_History_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Price_History_Id_seq"', 1, false);


--
-- TOC entry 4946 (class 0 OID 0)
-- Dependencies: 215
-- Name: Suppliers_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Suppliers_Id_seq"', 1, false);


--
-- TOC entry 4947 (class 0 OID 0)
-- Dependencies: 233
-- Name: Supplies_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Supplies_Id_seq"', 1, false);


--
-- TOC entry 4948 (class 0 OID 0)
-- Dependencies: 237
-- Name: Supplu_Usage_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Supplu_Usage_Id_seq"', 1, false);


--
-- TOC entry 4949 (class 0 OID 0)
-- Dependencies: 235
-- Name: Supply_Items_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Supply_Items_Id_seq"', 1, false);


--
-- TOC entry 4726 (class 2606 OID 83025)
-- Name: Benefits Benefits_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Benefits"
    ADD CONSTRAINT "Benefits_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4718 (class 2606 OID 82908)
-- Name: Customers Customers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Customers"
    ADD CONSTRAINT "Customers_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4724 (class 2606 OID 82925)
-- Name: Delivery Delivery_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Delivery"
    ADD CONSTRAINT "Delivery_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4714 (class 2606 OID 82898)
-- Name: Indications Indications_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Indications"
    ADD CONSTRAINT "Indications_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4716 (class 2606 OID 82954)
-- Name: Medicines_Indications_Map Medicines_Indications_Map_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Medicines_Indications_Map"
    ADD CONSTRAINT "Medicines_Indications_Map_pkey" PRIMARY KEY ("Medicines_Id") INCLUDE ("Indications_Id");


--
-- TOC entry 4712 (class 2606 OID 82891)
-- Name: Medicines Medicines_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Medicines"
    ADD CONSTRAINT "Medicines_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4722 (class 2606 OID 82937)
-- Name: Order_Details Order_Details_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Order_Details"
    ADD CONSTRAINT "Order_Details_pkey" PRIMARY KEY ("Order_Id") INCLUDE ("Medicine_Id");


--
-- TOC entry 4728 (class 2606 OID 83018)
-- Name: Order_Statuses Order_Statuses_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Order_Statuses"
    ADD CONSTRAINT "Order_Statuses_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4720 (class 2606 OID 82915)
-- Name: Orders Orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4730 (class 2606 OID 83063)
-- Name: Price_History Price_History_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Price_History"
    ADD CONSTRAINT "Price_History_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4710 (class 2606 OID 82884)
-- Name: Suppliers Suppliers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Suppliers"
    ADD CONSTRAINT "Suppliers_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4732 (class 2606 OID 83056)
-- Name: Supplies Supplies_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supplies"
    ADD CONSTRAINT "Supplies_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4734 (class 2606 OID 83037)
-- Name: Supply_Items Supply_Items_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supply_Items"
    ADD CONSTRAINT "Supply_Items_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4736 (class 2606 OID 83039)
-- Name: Supply_Usage Supply_Usage_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supply_Usage"
    ADD CONSTRAINT "Supply_Usage_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4744 (class 2606 OID 82948)
-- Name: Delivery Delivery_Order_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Delivery"
    ADD CONSTRAINT "Delivery_Order_Id_fkey" FOREIGN KEY ("Order_Id") REFERENCES public."Orders"("Id") NOT VALID;


--
-- TOC entry 4737 (class 2606 OID 82960)
-- Name: Medicines_Indications_Map Medicines_Indications_Map_Indications_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Medicines_Indications_Map"
    ADD CONSTRAINT "Medicines_Indications_Map_Indications_Id_fkey" FOREIGN KEY ("Indications_Id") REFERENCES public."Indications"("Id") NOT VALID;


--
-- TOC entry 4738 (class 2606 OID 82955)
-- Name: Medicines_Indications_Map Medicines_Indications_Map_Medicines_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Medicines_Indications_Map"
    ADD CONSTRAINT "Medicines_Indications_Map_Medicines_Id_fkey" FOREIGN KEY ("Medicines_Id") REFERENCES public."Medicines"("Id") NOT VALID;


--
-- TOC entry 4742 (class 2606 OID 82943)
-- Name: Order_Details Order_Details_Medicine_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Order_Details"
    ADD CONSTRAINT "Order_Details_Medicine_Id_fkey" FOREIGN KEY ("Medicine_Id") REFERENCES public."Medicines"("Id") NOT VALID;


--
-- TOC entry 4743 (class 2606 OID 82938)
-- Name: Order_Details Order_Details_Order_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Order_Details"
    ADD CONSTRAINT "Order_Details_Order_Id_fkey" FOREIGN KEY ("Order_Id") REFERENCES public."Orders"("Id") NOT VALID;


--
-- TOC entry 4739 (class 2606 OID 83026)
-- Name: Orders Orders_Benefit_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_Benefit_Id_fkey" FOREIGN KEY ("Benefit_Id") REFERENCES public."Benefits"("Id") NOT VALID;


--
-- TOC entry 4740 (class 2606 OID 82931)
-- Name: Orders Orders_Customer_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_Customer_Id_fkey" FOREIGN KEY ("Customer_Id") REFERENCES public."Customers"("Id") NOT VALID;


--
-- TOC entry 4741 (class 2606 OID 83019)
-- Name: Orders Orders_Status_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_Status_Id_fkey" FOREIGN KEY ("Status_Id") REFERENCES public."Order_Statuses"("Id") NOT VALID;


--
-- TOC entry 4745 (class 2606 OID 83064)
-- Name: Price_History Price_History_Medecine_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Price_History"
    ADD CONSTRAINT "Price_History_Medecine_Id_fkey" FOREIGN KEY ("Medecine_Id") REFERENCES public."Medicines"("Id") NOT VALID;


--
-- TOC entry 4746 (class 2606 OID 83031)
-- Name: Supplies Supplies_Supplier_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supplies"
    ADD CONSTRAINT "Supplies_Supplier_Id_fkey" FOREIGN KEY ("Supplier_Id") REFERENCES public."Suppliers"("Id") NOT VALID;


--
-- TOC entry 4747 (class 2606 OID 83045)
-- Name: Supplies Supplies_Supplier_Id_fkey1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supplies"
    ADD CONSTRAINT "Supplies_Supplier_Id_fkey1" FOREIGN KEY ("Supplier_Id") REFERENCES public."Suppliers"("Id") NOT VALID;


--
-- TOC entry 4748 (class 2606 OID 83050)
-- Name: Supply_Items Supply_Items_Medicine_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supply_Items"
    ADD CONSTRAINT "Supply_Items_Medicine_Id_fkey" FOREIGN KEY ("Medicine_Id") REFERENCES public."Medicines"("Id") NOT VALID;


--
-- TOC entry 4749 (class 2606 OID 83057)
-- Name: Supply_Items Supply_Items_Supply_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supply_Items"
    ADD CONSTRAINT "Supply_Items_Supply_Id_fkey" FOREIGN KEY ("Supply_Id") REFERENCES public."Supplies"("Id") NOT VALID;


--
-- TOC entry 4750 (class 2606 OID 83040)
-- Name: Supply_Usage Supply_Usage_Supply_Item_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Supply_Usage"
    ADD CONSTRAINT "Supply_Usage_Supply_Item_Id_fkey" FOREIGN KEY ("Supply_Item_Id") REFERENCES public."Supply_Items"("Id") NOT VALID;


-- Completed on 2024-10-01 11:39:36

--
-- PostgreSQL database dump complete
--

