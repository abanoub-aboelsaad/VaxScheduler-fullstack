import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Navbar from '../../components/Navbar';
import * as signalR from '@microsoft/signalr';

function ManageCenters() {
  const [centers, setCenters] = useState([]);

  useEffect(() => {
    // Establishing a connection to the SignalR hub
    const connection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:8080/VaccinationCenterHub') // URL of the SignalR hub
      .build();

    // Start the connection
    connection.start()
      .then(() => {
        console.log('SignalR connected.');

        // Request initial list of vaccination centers from the server
        connection.invoke('SendAllVaccinationCenters')
          .catch(error => console.error('Error invoking hub method:', error));

        // Listen for updates from the server and update state accordingly
        connection.on('ReceiveAllVaccinationCenters', vaccinationCenters => {
          setCenters(vaccinationCenters); // Update state with received data
        }); 
      })
      .catch(error => console.error('Error connecting to SignalR hub:', error));

  }, []);


  // useEffect(() => {
  //   fetch('http://localhost:8080/api/VaccinationCenter')
  //     .then(response => response.json())
  //     .then(data => setCenters(data))
  //     .catch(error => console.error('Error fetching data:', error));
  // }, []);

  return (
    <div>
      <Navbar />
      <link rel="stylesheet" type="text/css" href="/assets/managecenters/ManageCenters.css" />
      <div><img src="photo2.jpg" className='hi' alt="photo" /></div>

      <div>
        <h1 className='middleh'>Vaccine eligibility and staying up to date with your vaccination</h1>
        <p className='middlep'>The following locations are now accepting appointments to provide vaccinations. Appointment availability is dependent on vaccine supply and staffing. Maine CDC recommends making an appointment or calling the location to ensure vaccines can be given on a walk-in basis. Click on the links below to find out how to get a vaccine at a pharmacy near you.</p>
      </div>

      <div className='all'>
        {centers.map(center => (
          <div className="gallery" key={center.id}>
            <div className="content">
              <img src="photo.png" alt="photo" />
              <h3 className="header">{center.name}</h3>
              <Link to={`/CenterDetails/${center.id}`}>
                <button className="buy-3">See details</button>
              </Link>
            </div>
          </div>
        ))}
        <Link to="/AddVaccinationCenter">
         <button className="addcenter">ADD Center</button>
        </Link>
      </div>
    </div>
  );
}

export default ManageCenters;



