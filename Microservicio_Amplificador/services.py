from fastapi import HTTPException
from sqlalchemy.orm import Session
from models import Amplificador
from schemas import AmplificadorCreate

def get_amplificadores(db: Session):
    return db.query(Amplificador).all()

def get_amplificador(db: Session, amplificador_id: int):
    return db.query(Amplificador).filter(Amplificador.id == amplificador_id).first()

def create_amplificador(db: Session, amp: AmplificadorCreate):
    db_amp = get_amplificador(db, amp.id)
    
    if db_amp is not None:
        raise HTTPException(status_code=409, detail="Amplificador con este ID ya existe")
    
    db_amp = Amplificador(**amp.dict())
    db.add(db_amp)
    db.commit()
    db.refresh(db_amp)
    return db_amp

def update_amplificador(db: Session, amplificador_id: int, amp: AmplificadorCreate):
    db_amp = get_amplificador(db, amplificador_id)
    if db_amp:
        for key, value in amp.dict().items():
            setattr(db_amp, key, value)
        db.commit()
        db.refresh(db_amp)
    return db_amp

def delete_amplificador(db: Session, amplificador_id: int):
    db_amp = get_amplificador(db, amplificador_id)
    if db_amp:
        db.delete(db_amp)
        db.commit()
    return db_amp

def buscar_amplificadores(db: Session, marca: str = None):
    query = db.query(Amplificador)
    if marca:
        query = query.filter(Amplificador.marca.contains(marca))
    return query.all()
