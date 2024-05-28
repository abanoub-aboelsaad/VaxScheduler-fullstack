import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'; // Import useNavigate for navigation
import Navbar from '../../components/Navbar';
import axios from 'axios'; // Import axios for API requests

function ListCenters() {
  const [centers, setCenters] = useState([]);
  const navigate = useNavigate(); // Initialize useNavigate

  useEffect(() => {
    // Fetch vaccination centers from the API
    axios.get('http://localhost:8080/api/VaccinationCenter')
      .then(response => {
        // Update state with the fetched data
        setCenters(response.data);
      })
      .catch(error => {
        console.error('Error fetching centers:', error);
      });
  }, []); // Empty dependency array ensures this effect runs only once on component mount

  const handleDetailsClick = (centerId) => {
    // Navigate to PatientCenterDetails with center id
    navigate(`/PatientCenterDetails/${centerId}`);
  };

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
              {/* Use button for navigation */}
              <button className="buy-3" onClick={() => handleDetailsClick(center.id)}>See Details</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default ListCenters;
