import React from "react";
import { Modal, Button } from "react-bootstrap";

const CourseRemoveModal = () => {
  return (
    <React.Fragment>
      <Modal show={show} onHide={handleClose}>
        <Modal.Body>Archive: Are you sure?</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            No
          </Button>
          <Button variant="primary" onClick={handleClose}>
            Yes
          </Button>
        </Modal.Footer>
      </Modal>
    </React.Fragment>
  );
};

export default CourseRemoveModal;
