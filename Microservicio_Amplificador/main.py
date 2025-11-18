from fastapi import FastAPI, Depends, HTTPException
from sqlalchemy.orm import Session
from typing import Optional
from database import engine, SessionLocal
from models import Base
from schemas import AmplificadorCreate, AmplificadorRead
from services import (
    get_amplificadores, get_amplificador,
    create_amplificador, update_amplificador,
    delete_amplificador, buscar_amplificadores
)
Base.metadata.create_all(bind=engine)
app = FastAPI()

def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

@app.post("/amplificadores/", response_model=AmplificadorRead)
def crear_amp(amp: AmplificadorCreate, db: Session = Depends(get_db)):
    return create_amplificador(db, amp)

@app.get("/amplificadores/", response_model=list[AmplificadorRead])
def listar_amps(db: Session = Depends(get_db)):
    return get_amplificadores(db)

@app.get("/amplificadores/{amplificador_id}", response_model=AmplificadorRead)
def leer_amp(amplificador_id: int, db: Session = Depends(get_db)):
    amp = get_amplificador(db, amplificador_id)
    if amp is None:
        raise HTTPException(status_code=404, detail="Amplificador no encontrado")
    return amp

@app.put("/amplificadores/{amplificador_id}", response_model=AmplificadorRead)
def actualizar_amp(amplificador_id: int, amp: AmplificadorCreate, db: Session = Depends(get_db)):
    updated_amp = update_amplificador(db, amplificador_id, amp)
    if updated_amp is None:
        raise HTTPException(status_code=404, detail="Amplificador no encontrado")
    return updated_amp

@app.delete("/amplificadores/{amplificador_id}", status_code=204)
def borrar_amp(amplificador_id: int, db: Session = Depends(get_db)):
    deleted_amp = delete_amplificador(db, amplificador_id)
    if deleted_amp is None:
        raise HTTPException(status_code=404, detail="Amplificador no encontrado")
    return None

@app.get("/amplificadores/buscar/", response_model=list[AmplificadorRead])
def buscar_amps(marca: Optional[str] = None, db: Session = Depends(get_db)):
    return buscar_amplificadores(db, marca)
