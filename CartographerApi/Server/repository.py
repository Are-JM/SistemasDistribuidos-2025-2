from pymongo import MongoClient
from bson import ObjectId
from typing import Dict, Iterable, List
from datetime import datetime, timezone
from bson.errors import InvalidId


class CartographerRepository:
    def __init__(self, uri, db_name):
        self.client = MongoClient(uri)
        self.collection = self.client[db_name]["cartographers"]

    def exists_by_name(self, name):
        return self.collection.find_one({"name": name}) is not None

    def create(self, req):
            insert = self.collection.insert_one({
                "name": req.name,
                "company": req.company,
                "age": req.age,
                "created_at": datetime.now(timezone.utc)
            })

            doc = self.collection.find_one({"_id": insert.inserted_id})
            return self.map(doc)

    def get_by_id(self, id):
        try:
             oid = ObjectId(id)
        except InvalidId:
            return None
        
        doc = self.collection.find_one({"_id": oid})
        if not doc:
            return None
        return self.map(doc)

    def get_by_name(self, name):
        query ={
             "name":{
                    "$regex": name,
                    "$options": "i"
             }
        }
        for doc in self.collection.find(query):
            yield self.map(doc)

    def map(self, doc):
        return {
            "id": str(doc["_id"]),
            "name": doc.get("name", ""),
            "company": doc.get("company", ""),
            "age": doc.get("age", 0),
            "created_at": doc.get(
                "created_at",
                datetime.now(timezone.utc)
            )
        }