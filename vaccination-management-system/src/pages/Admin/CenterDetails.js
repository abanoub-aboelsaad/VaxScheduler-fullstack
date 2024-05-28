import React, { useState, useEffect } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import Navbar from '../../components/Navbar';

function CenterDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [center, setCenter] = useState(null);
  const [vaccines, setVaccines] = useState([]);

  useEffect(() => {
    fetch(`http://localhost:8080/api/VaccinationCenter/${id}`)
      .then(response => response.json())
      .then(data => setCenter(data))
      .catch(error => console.error('Error fetching center data:', error));

    fetch(`http://localhost:8080/api/vaccines/by-center/${id}`)
      .then(response => response.json())
      .then(data => setVaccines(data))
      .catch(error => console.error('Error fetching vaccines:', error));
  }, [id]);

  const handleDeleteCenter = () => {
    fetch(`http://localhost:8080/api/VaccinationCenter/${id}`, {
      method: 'DELETE'
    })
    .then(() => {
      // Redirect to some page after deletion
    })
    .catch(error => console.error('Error deleting center:', error));
  };

  const handleModifyVaccine = (vaccineId) => {
    navigate(`/VaccineSDetails/${vaccineId}`);
  };

  const handleAddVaccine = () => {
    navigate(`/AddVaccine/${id}`);
  };

  return (
    <div>
      <Navbar />
      <link rel="stylesheet" href="/assets/centerdetails/CenterDetails.css" />

      <h1>Center Details:</h1>
      {center && (
        <div className="container">
          <div className="product-card">
            <div className="image">
              <img src="https://encrypted-tbn2.gstatic.com/licensed-image?q=tbn:ANd9GcTmAjfYiyYhKFdlJMZEIRQs_d-zikMcv7HP6Na9fMQAV_42s2oxipTOmbtxMSRSwCydzs86OFtvUGQeVbsK1N3COe7w7XbLzZ1LtEk4gw" alt="center" />
            </div>
            <div className="card-content">
              <h3>{center.name}</h3>
              <p>{center.address}</p>
              <div className="store-purchase">
                <div className="price">{center.contactInfo}</div>
                <div className="buy">
                  <Link to={`/UpdateCenter/${id}`} className='update'>Update</Link>
                  <button className='delete' onClick={handleDeleteCenter}>DELETE</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}

      <div>
        <h1 className='middleh'>Manage this center's vaccines</h1>
      </div>

      <div className='all'>
        {Array.isArray(vaccines) && vaccines.map(vaccine => (
          <div className="gallery" key={vaccine.id}>
            <div className="contenttt">
              <img className='photocent' src="/photo.png" alt="vaccine" />
              <h3 className="header">{vaccine.name}</h3>
              <button className="buy-3" onClick={() => handleModifyVaccine(vaccine.id)}>MODIFY Vaccine</button>
            </div>
          </div>
        ))}
      </div>

      <div className='buton'>
        <button className='add' onClick={handleAddVaccine}>ADD VACCINE</button>
      </div>
    </div>
  );
}

export default CenterDetails;
