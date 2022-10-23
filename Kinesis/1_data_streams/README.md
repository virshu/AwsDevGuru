# Python Kinesis Data Stream Example

## Outputs

**Create Stream**
```
ubuntu@adgu$ python3 create_stream.py 

> Current Streams
  None

> Creating Test Stream
  Response status code: 200

> Waiting for stream status: ACTIVE
  Status: CREATING
  Status: CREATING
  Status: CREATING
  Status: CREATING
  Status: CREATING
  Status: CREATING
  Status: ACTIVE

> Current Streams
  TestStream

```

**Producer**
```
ubuntu@adgu$ python3 producer.py 

> Loading words.json
  Loaded 436 words.

> Sending all words to stream.
  Sending: abroad
  Sending: accouchement
  Sending: advertisement
  Sending: afeard/afeared
  Sending: affright
  Sending: ague
  Sending: aliment
  Sending: ambuscade
  Sending: animalcule
  Sending: apothecary
  Sending: appetency
  Sending: assay
  Sending: asunder
<cut>
```

**Consumer**
```
ubuntu@adgu$ python3 consumer.py
> Polling stream every 100 ms
  Got record: b'{"word": "abroad", "definition": "out of doors"}'
  Got record: b'{"word": "accouchement", "definition": "birthing"}'
  Got record: b'{"word": "advertisement", "definition": "a notice to readers in a book"}'
  Got record: b'{"word": "afeard/afeared", "definition": "frightened"}'
  Got record: b'{"word": "affright", "definition": "frighten (someone)"}'
  Got record: b'{"word": "ague", "definition": "malaria or a similar illness"}'
  Got record: b'{"word": "aliment", "definition": "food; nourishment"}'
  Got record: b'{"word": "ambuscade", "definition": "an ambush"}'
  Got record: b'{"word": "animalcule", "definition": "a microscopic animal"}'
  Got record: b'{"word": "apothecary", "definition": "a person who prepared and sold medicine"}'
  Got record: b'{"word": "appetency", "definition": "a longing or desire"}'
<cut>
```

**Delete Stream**
```
ubuntu@adgu$ python3 delete_stream.py 

> Current Streams
  TestStream

> Deleting Test Stream
  Response status code: 200

> Waiting for deletion
  Status: DELETING
  Status: DELETING
  Status: DELETING

> Current Streams
  None
```

