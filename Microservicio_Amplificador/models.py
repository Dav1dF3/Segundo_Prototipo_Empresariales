from sqlalchemy import Column, Integer, String, Float, Date
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()

class Amplificador(Base):
    __tablename__ = 'amplificadores'
    id = Column(Integer, primary_key=True, index=True, autoincrement=False)
    marca = Column(String, index=True)
    modelo = Column(String)
    potencia = Column(Float)
    tipo_tubo = Column(String)
    fecha_fabricacion = Column(Date)