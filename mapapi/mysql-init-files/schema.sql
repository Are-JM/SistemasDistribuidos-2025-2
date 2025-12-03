CREATE DATABASE IF NOT EXISTS countries_db;
USE countries_db;
create table country (
        id bigint not null,
        population bigint,
        capital varchar(50) not null,
        currency varchar(50) not null,
        name varchar(50) not null,
        primary key (id)
    );
    create table country_seq (
        next_val bigint
    );
