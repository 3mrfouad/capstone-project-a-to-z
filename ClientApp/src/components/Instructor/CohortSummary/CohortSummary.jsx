import React from "react";
import { Table, Container, Button, Modal } from "react-bootstrap";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { cohortSummaryInstructor } from "../../../actions/instructorActions";
import { Link } from "react-router-dom";
import Loader from "../../shared/Loader/Loader";

const CohortSummaryInstructor = () => {
  const dispatch = useDispatch();
  const { cohorts, loading } = useSelector(
    (state) => state.cohortSummaryInstructor
  );
  useEffect(() => {
    dispatch(cohortSummaryInstructor());
  }, [dispatch]);
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <React.Fragment>
          <Container>
            <Table>
              <thead>
                <tr>
                  <th>Cohort Name</th>
                  <th>Capacity</th>
                  <th>Mode</th>
                  <th>Start Date</th>
                  <th>End Date</th>
                  <th>City</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {cohorts.map((cohort, index) => (
                  <tr key={index}>
                    <td>{cohort.name}</td>
                    <td>{cohort.capacity}</td>
                    <td>{cohort.modeOfTeaching}</td>
                    <td>{cohort.startDate}</td>
                    <td>{cohort.endDate}</td>
                    <td>{cohort.city}</td>
                    <td>
                      <Link to={`/cohortedit/${cohort.cohortId}`}>Edit</Link> |{" "}
                      <Link to="#" onClick={handleShow}>
                        {" "}
                        Retire{" "}
                      </Link>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <button type="button" className="btn btn-link">
              Back
            </button>{" "}
            <Button href="/cohortcreate" className="float-right mr-3">
              Create Cohort
            </Button>
            <Button href="/cohortcreate" className="float-right mr-3">
              Register Users
            </Button>{" "}
            <Button href="/managecourse" className="float-right mr-3">
              Manage Course
            </Button>{" "}
          </Container>
          <Modal show={show} onHide={handleClose}>
            <Modal.Body>Retire: Are you sure?</Modal.Body>
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
      )}
    </React.Fragment>
  );
};

export default CohortSummaryInstructor;
