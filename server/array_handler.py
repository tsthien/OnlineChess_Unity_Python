
import json
from json import JSONEncoder
import numpy

class NumpyArrayEncoder(JSONEncoder):
    def default(self, obj):
        if isinstance(obj, numpy.ndarray):
            return obj.tolist()
        return JSONEncoder.default(self, obj)
def array_to_json(turn, win, team, a1, a2, a3, a4, a5, a6, a7, a8):
    return json.dumps({'turn' : turn, 'win': win, 'team': team, 'row1': a1,
                       'row2': a2, 'row3': a3, 'row4': a4, 'row5': a5, 'row6': a6, 'row7': a7, 'row8': a8}, cls=NumpyArrayEncoder)