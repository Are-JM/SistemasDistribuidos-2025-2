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
