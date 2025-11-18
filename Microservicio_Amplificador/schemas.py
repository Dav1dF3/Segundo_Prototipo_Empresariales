from pydantic import BaseModel
from datetime import date
from typing import Optional

class AmplificadorCreate(BaseModel):
    id :int
    marca: str
    modelo: str
    potencia: float
    tipo_tubo: str
    fecha_fabricacion: date

class AmplificadorRead(AmplificadorCreate):
    pass
