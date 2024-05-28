import React, { useState } from 'react';
import Navbar from '../../components/Navbar';
import axios from 'axios';
import { useParams } from 'react-router-dom'; // Import useParams hook
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function UploadCertificate() {
  const [file, setFile] = useState(null);
  // Use useParams hook to get the userId from URL parameter
  const { userId } = useParams();
console.log(userId)
  const handleFileChange = (e) => {
    setFile(e.target.files[0]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    try {
      const formData = new FormData();
      formData.append('file', file);
  
      const response = await axios.post(
        `http://localhost:8080/api/Certificates/upload?userId=${userId}`,
        formData,
        {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        }
      );
  
      console.log('Certificate uploaded successfully:', response.data);
      toast.success('Certificate uploaded successfully');
      // You can add further actions here like showing a success message or redirecting the user
    } catch (error) {
      console.error('Error uploading certificate:', error);
      toast.error('Error uploading certificate');
      // Handle error scenario here, show an error message to the user, etc.
    }
  };
  
  return (
    <div>
      <Navbar />
      <link rel="stylesheet" href="/assets/uploadcertificate/UploadCertificate.css"></link> 
      <h1>Upload Certificate</h1>
      <div className='upload'>
        <form onSubmit={handleSubmit}>
          <input type="file" name="file" onChange={handleFileChange} />
          <button type="submit">Submit</button>
        </form>
      </div>
    </div>
  );
}

export default UploadCertificate;
