import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom'; // Import useNavigate from react-router-dom
import Navbar from '../../components/Navbar';
import axios from 'axios';

function ManageCertificate() {
  const [reservations, setReservations] = useState([]);
  const navigate = useNavigate(); 

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('http://localhost:8080/api/Reservation/all/users/with-approved-second-dose');
        const reservationsWithCertificates = await Promise.all(
          response.data.map(async (reservation) => {
            try {
             
              await axios.get(`http://localhost:8080/api/Certificates/${reservation.id}/photo`);
             
              return null;
            } catch (error) {
           
              return reservation;
            }
          })
        );
      
        const filteredReservations = reservationsWithCertificates.filter(reservation => reservation !== null);
        setReservations(filteredReservations);
      } catch (error) {
        console.error('Error fetching reservations:', error);
      }
    };
  
    fetchData();
  }, []);

  const handleUploadCertificate = (userId) => {
    // Navigate to the UploadCertificate page with the userId parameter
    navigate(`/UploadCertificate/${userId}`);
  };

  return (
    <div>
      <Navbar />
      <div className='boddyyy'>
        <link rel="stylesheet" type="text/css" href="/assets/manageusers/ManageUsers.css" />
        <main className="table" id="customers_table">
          <section className="table__header">
            <h1>ALL the patients that are ready to upload certificate</h1>
          </section>
          <section className="table__body">
            <table>
              <thead>
                <tr>
                  <th>Patient ID</th>
                  <th>Patient Name</th>
                  <th>Vaccine</th>
                  <th>Upload Certificate</th>
                </tr>
              </thead>
              <tbody>
                {reservations.map(reservation => (
                  <tr key={reservation.id}>
                    <td>{reservation.id}</td>
                    <td><strong>{reservation.userName}</strong></td>
                    <td>{reservation.vaccineName}</td>
                    <td>
                      
                      <button className="status view" onClick={() => handleUploadCertificate(reservation.id)}>Upload</button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </section>
        </main>
      </div>
    </div>
  )
}

export default ManageCertificate;
