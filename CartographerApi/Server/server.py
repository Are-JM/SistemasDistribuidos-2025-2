import grpc
import Cartographer_pb2
import Cartographer_pb2_grpc
from concurrent import futures
import os
from google.protobuf.timestamp_pb2 import Timestamp
from repository import CartographerRepository
from bson import ObjectId
from bson.errors import InvalidId
import logging

_ONE_DAY_IN_SECONDS = 60 * 60 * 24

class CartographerService(Cartographer_pb2_grpc.CartographerServiceServicer):
    def __init__(self):
        mongo_uri = os.getenv("MONGO_URI")
        db_name = os.getenv("DB_NAME")
        self.repo = CartographerRepository(mongo_uri, db_name)

    def CreateCartographers(self, request_iterator, context):
        cartographersCreated = []

        for req in request_iterator:
            if req.age <= 18:
                continue
            
            if self.repo.exists_by_name(req.name):
                continue
            
            carto = self.repo.create(req)
            ts = Timestamp()
            ts.FromDatetime(carto["created_at"])

            cartographersCreated.append(
                Cartographer_pb2.CartographerResponse(
                    id=carto["id"],
                    name=carto["name"],
                    company=carto["company"],
                    age=carto["age"],
                    created_at=ts
                )
            )

        return Cartographer_pb2.CreateCartographerResponse(cartographers = cartographersCreated, success_count=len(cartographersCreated))
        
    def GetById(self, request, context):
        if not request.id or not request.id.strip():
           context.abort(grpc.StatusCode.INVALID_ARGUMENT, "ID cannot be null")
        
        try:
            ObjectId(request.id)
        except InvalidId:
            context.abort(grpc.StatusCode.INVALID_ARGUMENT, "Invalid ID format")
        
        cartographer = self.repo.get_by_id(request.id)
        if not cartographer:
            context.abort(grpc.StatusCode.NOT_FOUND, "Cartographer not found")

        ts = Timestamp()
        ts.FromDatetime(cartographer["created_at"])

        return Cartographer_pb2.CartographerResponse(
            id=cartographer["id"],
            name=cartographer["name"],
            company=cartographer["company"],
            age=cartographer["age"],
            created_at=ts
        )

    def GetCartographersByName(self, request, context):

        if not request.name or not request.name.strip():
            context.abort(grpc.StatusCode.INVALID_ARGUMENT, "Name cannot be null")
            
        cartographers = self.repo.get_by_name(request.name)
        for carto in cartographers:
            ts = Timestamp()
            ts.FromDatetime(carto["created_at"])

            yield Cartographer_pb2.CartographerResponse(
                id=carto["id"],
                name=carto["name"],
                company=carto["company"],
                age=carto["age"],
                created_at=ts
            )

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    Cartographer_pb2_grpc.add_CartographerServiceServicer_to_server(
        CartographerService(), server
    )
    
    logging.basicConfig(level=logging.INFO)
    logging.info("Starting Cartographer gRPC server on 0.0.0.0:50051")
    server.add_insecure_port('[::]:50051')
    server.start()
    server.wait_for_termination()

if __name__ == '__main__':
    serve()
