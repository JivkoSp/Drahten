import uuid
import hashlib

class UUIDGenerator:
    # Define a namespace for generating UUIDs
    NAMESPACE = uuid.UUID('12345678-1234-5678-1234-567812345678')

    @staticmethod
    def generate_uuid_from_content(content):
        # Generate a UUID based on document content and remove dashes.
        name = hashlib.sha1(content.encode('utf-8')).hexdigest()
        uuid_with_dashes = uuid.uuid5(UUIDGenerator.NAMESPACE, name)
        return str(uuid_with_dashes).replace('-', '')