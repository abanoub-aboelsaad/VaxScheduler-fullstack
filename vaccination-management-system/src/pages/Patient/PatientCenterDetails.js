import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import Navbar from '../../components/Navbar';

function PatientCenterDetails() {
  const { id } = useParams(); // Extracting center ID from URL parameter
  const navigate = useNavigate(); // Navigation function
  const [centerData, setCenterData] = useState(null);
  const [vaccines, setVaccines] = useState([]);

  useEffect(() => {
    const fetchCenterData = async () => {
      try {
        const centerResponse = await axios.get(`http://localhost:8080/api/VaccinationCenter/${id}`);
        setCenterData(centerResponse.data);
      } catch (error) {
        console.error('Error fetching center data:', error);
      }
    };

    const fetchVaccineData = async () => {
      try {
        const vaccineResponse = await axios.get(`http://localhost:8080/api/vaccines/by-center/${id}`);
        setVaccines(vaccineResponse.data);
      } catch (error) {
        console.error('Error fetching vaccine data:', error);
      }
    };

    fetchCenterData();
    fetchVaccineData();
  }, [id]); // Trigger useEffect whenever the ID parameter changes

  const handleReserveVaccine = (vaccineId) => {
    navigate(`/PatientVaccineDetails/${vaccineId}`);
  };

  return (
    <div>
      <Navbar />
      <link rel="stylesheet" href="/assets/centerdetails/CenterDetails.css"></link>
     
      <h1>Center Details:</h1>
      {centerData && (
        <div className="container">
          <div className="product-card">
            <div className="image">
              <img src="https://encrypted-tbn2.gstatic.com/licensed-image?q=tbn:ANd9GcTmAjfYiyYhKFdlJMZEIRQs_d-zikMcv7HP6Na9fMQAV_42s2oxipTOmbtxMSRSwCydzs86OFtvUGQeVbsK1N3COe7w7XbLzZ1LtEk4gw" alt="" />
            </div>
            <div className="card-content">
              <h3>{centerData.name} VACCINATION CENTER</h3>
              <p>{centerData.address}</p>
              <div className="store-purchase">
                <div className="price">{centerData.contactInfo}</div>
              </div>
            </div>
          </div>
        </div>
      )}

      <div>
        <h1 className="middleh">Here are your available vaccines for reservation</h1>
      </div>

      <div className="all">
        {vaccines.map((vaccine) => (
          <div className="gallery" key={vaccine.id}>
            <div className="contenttt">
              <img className="photocent" src="/photo.png" alt="photo" />
              <h3 className="header">{vaccine.name}</h3>
              <button className="buy-3" onClick={() => handleReserveVaccine(vaccine.id)}>Reserve Vaccine</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default PatientCenterDetails;
