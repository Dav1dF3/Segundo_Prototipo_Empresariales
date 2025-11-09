package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.dto.FiltroInstrumentoDTO;
import com.unibague.poctiendainstrumentos.model.Guitarra;
import com.unibague.poctiendainstrumentos.model.Instrumento;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.TypedQuery;

import java.util.List;

public class InstrumentoRepositoryImpl implements  InstrumentoRepositoryCustom {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public List<Instrumento> filtrarInstrumentos(FiltroInstrumentoDTO filtro) {
        StringBuilder jpql = new StringBuilder("SELECT i FROM Instrumento i WHERE 1=1 ");

        if (filtro.getNombre() != null && !filtro.getNombre().isEmpty()) {
            jpql.append("AND LOWER(i.nombre) LIKE LOWER(CONCAT('%', :nombre, '%')) ");
        }
        if (filtro.getMarca() != null && !filtro.getMarca().isEmpty()) {
            jpql.append("AND LOWER(i.marca) = LOWER(:marca) ");
        }
        if (filtro.getPrecioMin() != null) {
            jpql.append("AND i.precioBase >= :precioMin ");
        }
        if (filtro.getPrecioMax() != null) {
            jpql.append("AND i.precioBase <= :precioMax ");
        }
        if (filtro.getStockMin() != null) {
            jpql.append("AND i.stock >= :stockMin ");
        }
        if (filtro.getStockMax() != null) {
            jpql.append("AND i.stock <= :stockMax ");
        }

        TypedQuery<Instrumento> query = entityManager.createQuery(jpql.toString(), Instrumento.class);

        if (filtro.getNombre() != null && !filtro.getNombre().isEmpty()) {
            query.setParameter("nombre", filtro.getNombre());
        }
        if (filtro.getMarca() != null && !filtro.getMarca().isEmpty()) {
            query.setParameter("marca", filtro.getMarca());
        }
        if (filtro.getPrecioMin() != null) {
            query.setParameter("precioMin", filtro.getPrecioMin());
        }
        if (filtro.getPrecioMax() != null) {
            query.setParameter("precioMax", filtro.getPrecioMax());
        }
        if (filtro.getStockMin() != null) {
            query.setParameter("stockMin", filtro.getStockMin());
        }
        if (filtro.getStockMax() != null) {
            query.setParameter("stockMax", filtro.getStockMax());
        }

        return query.getResultList();
    }
}
