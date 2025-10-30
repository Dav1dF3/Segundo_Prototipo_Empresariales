/*
 * Clase: ServicioInstrumento
 * Proyecto: PoC Tienda de Instrumentos
 * Paquete: com.unibague.poctiendainstrumentos.service
 *
 * Descripción:
 *   Clase de servicio que gestiona la colección de instrumentos musicales en la tienda.
 *   Implementa el patrón Singleton para asegurar que solo exista una instancia
 *   de este servicio en toda la aplicación.
 */
package com.unibague.poctiendainstrumentos.service;

import com.unibague.poctiendainstrumentos.dto.FiltroInstrumentoDTO;
import com.unibague.poctiendainstrumentos.model.Funda;
import com.unibague.poctiendainstrumentos.model.Guitarra;
import com.unibague.poctiendainstrumentos.model.Instrumento;
import com.unibague.poctiendainstrumentos.model.Teclado;
import com.unibague.poctiendainstrumentos.repository.FundaRepository;
import com.unibague.poctiendainstrumentos.repository.GuitarraRepository;
import com.unibague.poctiendainstrumentos.repository.InstrumentoRepository;
import com.unibague.poctiendainstrumentos.repository.TecladoRepository;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.stereotype.Service;

import java.util.*;
import java.util.function.Predicate;

/**
 * Servicio central para la gestión de instrumentos musicales en la tienda.
 *
 * <p>
 * Implementa un <b>patrón Singleton</b> para garantizar que solo exista una instancia
 * activa en la aplicación, facilitando la administración y persistencia de los datos.
 * </p>
 *
 * <p>
 * Permite agregar, buscar, listar, editar y eliminar instrumentos, tanto de forma general
 * como específica por tipo (guitarras, teclados). Además, gestiona las fundas asociadas
 * a guitarras permitiendo su adición, edición y eliminación, y provee métodos para filtrar
 * instrumentos según criterios flexibles.
 * </p>
 *
 * <p>
 * Es compatible con el uso en aplicaciones Spring gracias a la anotación {@code @Service}.
 * </p>
 *
 * <p>
 * Las operaciones principales incluyen:
 * <ul>
 *   <li>Creación (agregar instrumento, agregar fundas)</li>
 *   <li>Lectura (listar todos, listar por tipo, buscar por código, filtrar por criterios)</li>
 *   <li>Actualización (editar instrumento, editar funda)</li>
 *   <li>Eliminación (eliminar instrumento, eliminar funda)</li>
 * </ul>
 * </p>
 *
 * <p>
 * Incluye validaciones y excepciones detalladas para evitar estados inconsistentes
 * o que se añadan elementos duplicados o inválidos.
 * </p>
 *
 * @author Jorge
 * @version 1.0
 * @since 2025
 */
@Service
public class ServicioInstrumento implements IServicioInstrumento {

    @Autowired
    private InstrumentoRepository instrumentoRepository;

    @Autowired
    private TecladoRepository tecladoRepository;

    @Autowired
    private GuitarraRepository guitarraRepository;

    @Autowired
    private FundaRepository fundaRepository;


    /**
     * Agrega un instrumento a la colección.
     * Garantiza que no se repita el código.
     * Si la guitarra tiene fundas, asocia la guitarra (owner) a cada funda.
     *
     * @param instrumento Instrumento a agregar.
     * @throws IllegalArgumentException si el instrumento es nulo.
     */
    @Override
    @Transactional
    public void agregarInstrumento(Instrumento instrumento) {
        if (instrumento == null) {
            throw new IllegalArgumentException("El instrumento no puede ser nulo");
        }
        if (instrumentoRepository.existsById(instrumento.getCodigo())) {
            throw new DataIntegrityViolationException("El instrumento ya existe");
        }
        instrumentoRepository.save(instrumento);
    }

    /**
     * Lista todos los instrumentos disponibles.
     * Devuelve una vista inmutable, solo lectura.
     *
     * @return lista inmutable de instrumentos.
     */
    @Override
    @Transactional
    public List<Instrumento> listarInstrumentos() {
        return Collections.unmodifiableList(instrumentoRepository.findAll());
    }

    /**
     * Lista sólo los instrumentos que son guitarras.
     *
     * @return lista de guitarras.
     */
    @Override
    @Transactional
    public List<Guitarra> listarGuitarras() {
        return Collections.unmodifiableList(guitarraRepository.findAll());
    }

    /**
     * Lista sólo los instrumentos que son teclados.
     *
     * @return lista de teclados.
     */
    @Override
    @Transactional
    public List<Teclado> listarTeclados() {
       return Collections.unmodifiableList(tecladoRepository.findAll());
    }

    /**
     * Busca un instrumento específico por código.
     *
     * @param codigo código único a buscar
     * @return Optional con el instrumento si existe
     */
    @Override
    @Transactional
    public Optional<Instrumento> buscarInstrumento(long codigo) {
        return instrumentoRepository.findById(codigo);
    }

    /**
     * Edita y reemplaza los datos de un instrumento, identificado por código.
     *
     * @param codigo código identificador
     * @param instrumento nuevo objeto con datos a actualizar
     * @throws NoSuchElementException si no existe instrumento con ese código
     */
    @Override
    @Transactional
    public void editarInstrumento(long codigo, Instrumento instrumento) {
        if (!instrumentoRepository.existsById(codigo)) {
            throw new NoSuchElementException("No se encontró un instrumento con el código: " + codigo);
        }
        instrumento.setCodigo(codigo);
        instrumentoRepository.save(instrumento);
    }

    /**
     * Elimina un instrumento por código único.
     *
     * @param codigo código identificador
     * @throws NoSuchElementException si no existe instrumento con ese código
     */
    @Override
    @Transactional
    public void eliminarInstrumento(long codigo) {
        if (!instrumentoRepository.existsById(codigo)) {
            throw new NoSuchElementException("No se encontró un instrumento con el código: " + codigo);
        }
        instrumentoRepository.deleteById(codigo);
    }

    /**
     * Agrega una lista de fundas a una guitarra por su código.
     *
     * @param codigoGuitarra código de la guitarra destino
     * @param fundas lista de fundas a agregar
     * @throws NoSuchElementException si no existe guitarra
     * @throws IllegalArgumentException si el código no corresponde a una guitarra
     */
    @Override
    @Transactional
    public void agregarFundas(long codigoGuitarra, List<Funda> fundas)
    {
        if (fundas == null || fundas.isEmpty()) {
            throw new IllegalArgumentException("La lista de fundas no puede ser nula ni vacía");
        }

        Optional<Guitarra> guitarra = guitarraRepository.findById(codigoGuitarra);
        if (guitarra.isEmpty()) {
            throw new NoSuchElementException("No se encontró una guitarra con el código: " + codigoGuitarra);
        }

        guitarra.get().agregarFundas(fundas);

        guitarraRepository.save(guitarra.get());
    }

    @Override
    @Transactional
    public List<Funda> listarFundas() {
        return Collections.unmodifiableList(fundaRepository.findAll());
    }

    @Override
    @Transactional
    public Optional<Funda> buscarFunda(long codigoFunda)
    {
        return fundaRepository.findById(codigoFunda);
    }

    /**
     * Edita los datos de una funda asociada a una guitarra específica.
     *
     * @param codigoGuitarra código de la guitarra
     * @param codigoFunda código de la funda a editar
     * @param funda nueva funda con datos actualizados
     * @throws NoSuchElementException si no existe guitarra o funda
     * @throws IllegalArgumentException si el código no corresponde a una guitarra
     */
    @Override
    @Transactional
    public void editarFunda(long codigoGuitarra, long codigoFunda, Funda funda) {
        if (funda == null) {
            throw new IllegalArgumentException("La funda no puede ser nula");
        }
        Optional<Guitarra> guitarra = guitarraRepository.findById(codigoGuitarra);
        if (guitarra.isEmpty()) {
            throw new NoSuchElementException("No se encontró una guitarra con el código: " + codigoGuitarra);
        }
        guitarra.get().editarFunda(codigoFunda, funda);

        guitarraRepository.save(guitarra.get());
    }

    /**
     * Elimina una funda de una guitarra específica.
     *
     * @param codigoGuitarra código de la guitarra
     * @param codigoFunda código de la funda a eliminar
     * @throws NoSuchElementException si no existe guitarra o funda
     * @throws IllegalArgumentException si el código no corresponde a una guitarra
     */
    @Override
    @Transactional
    public void eliminarFunda(long codigoGuitarra, long codigoFunda) {
        Optional<Guitarra> guitarra = guitarraRepository.findById(codigoGuitarra);
        if (guitarra.isEmpty()) {
            throw new NoSuchElementException("No se encontró una guitarra con el código: " + codigoGuitarra);
        }
        guitarra.get().eliminarFunda(codigoFunda);

        guitarraRepository.save(guitarra.get());
    }



    /**
     * Filtra la lista de instrumentos según los criterios proporcionados en un DTO.
     * Cada filtro es opcional y el método compone dinámicamente los predicados.
     *
     * @param filtro objeto DTO con los filtros (nombre, marca, precio, stock, tipo, sensibilidad, etc.)
     * @return lista de instrumentos que cumplen los criterios.
     */
    @Override
    public List<Instrumento> filtrarInstrumentos(FiltroInstrumentoDTO filtro) {
        //PENDIENTE
        return null;
    }
}
