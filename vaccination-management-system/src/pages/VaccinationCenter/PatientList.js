import React, { useState, useEffect } from 'react';
import Navbar from '../../components/Navbar';
import axios from 'axios';

function PatientList() {
  const [reservations, setReservations] = useState([]);

  useEffect(() => {
    // Fetch reservation data from the API
    axios.get('http://localhost:8080/api/Reservation/all/reservations')
      .then(response => {
        setReservations(response.data);
      })
      .catch(error => {
        console.error('Error fetching reservations:', error);
      });
  }, []); // Add reservations as a dependency

  const handleApproveDose = (id, doseNumber) => {
    // Send a request to approve the specified dose for the reservation with the given ID
    axios.post(`http://localhost:8080/api/Reservation/${id}/approve`, { doseNumber })
      .then(response => {
        // Log success message or perform any other action if needed
        console.log(`${doseNumber} dose approved for reservation ${id}:`, response.data);
        // Remove the approved reservation from the reservations state
        setReservations(prevReservations => {
          return prevReservations.filter(reservation => reservation.id !== id);
        });
      })
      .catch(error => {
        console.error(`Error approving ${doseNumber} dose for reservation ${id}:`, error);
      });
  };

  return (
    <div>
      <Navbar />
      <div className='boddyyy'>
        <link rel="stylesheet" type="text/css" href="/assets/manageusers/ManageUsers.css" />
        <main className="table" id="customers_table">
          <section className="table__header">
            <h1>Patient List</h1>
          </section>
          <section className="table__body">
            <table>
              <thead>
                <tr>
                  <th> </th>
                  <th> Patient Name </th>
                  <th> Vaccine </th>
                  <th> DoseNumber </th>
                  <th> Approve Dose </th>
                  {/* <th> Upload Certificate </th> */}
                </tr>
              </thead>
              <tbody>
                {reservations.map(reservation => (
                  <tr key={reservation.id}>
                    <td>{reservation.id}</td>
                    <td><strong>{reservation.userName}</strong></td>
                    <td>{reservation.vaccineName}</td>
                    <td>{reservation.doseNumber}</td>
                    <td>
                      <button className="status delivered" onClick={() => handleApproveDose(reservation.id, 'First')}>
                        Approve
                      </button>
                    </td>
                    {/* <td>
                      <button className="status view">Upload</button> 
                    </td> */}
                  </tr>
                ))}
              </tbody>
            </table>
          </section>
        </main>
      </div>
    </div>
  );
}

export default PatientList;
