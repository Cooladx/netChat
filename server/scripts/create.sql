-- Connect to netchatdb
-- CREATE DATABASE netchatdb;  -- if not already created
-- \c netchatdb

-- Drop tables if they exist 
DROP TABLE IF EXISTS message;
DROP TABLE IF EXISTS room;
DROP TABLE IF EXISTS users;

-- Users table
CREATE TABLE users (
    userid VARCHAR PRIMARY KEY,
    username VARCHAR NOT NULL,
    password VARCHAR NOT NULL,
    rkey VARCHAR
);

-- Room table
CREATE TABLE room (
    roomid VARCHAR PRIMARY KEY,
    roomname VARCHAR NOT NULL,
    creatorid VARCHAR NOT NULL,
    userlist VARCHAR
);

-- Message table
CREATE TABLE message (
    messageid SERIAL PRIMARY KEY,
    roomid VARCHAR NOT NULL REFERENCES room(roomid),
    userid VARCHAR NOT NULL REFERENCES users(userid),
    content TEXT NOT NULL,
    timestamp TIMESTAMP DEFAULT NOW()
);



