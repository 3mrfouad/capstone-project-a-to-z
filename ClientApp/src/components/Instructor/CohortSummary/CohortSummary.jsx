import React from "react";
import { Table, Container, Button, Modal } from "react-bootstrap";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  cohortSummaryInstructor,
  archiveCohort,
} from "../../../actions/instructorActions";
import { Link } from "react-router-dom";
import Loader from "../../shared/Loader/Loader";
import { Navbar } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";

const CohortSummaryInstructor = ({ history }) => {
  const dispatch = useDispatch();
  const { cohorts, loading } = useSelector(
    (state) => state.cohortSummaryInstructor
  );
  const { success } = useSelector((state) => state.cohortArchive);
  useEffect(() => {
    dispatch(cohortSummaryInstructor());
  }, [dispatch, success]);
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const onArchive = (id) => {
    setShow(false);
    dispatch(archiveCohort(id));
  };
  const goBack = () => {
    history.goBack();
  };
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
                {cohorts
                  .filter((cohort) => cohort.archive == false)
                  .map((cohort, index) => (
                    <tr key={index}>
                      <td>
                        <Link to={`/coursesummary/${cohort.cohortId}`}>
                          {cohort.name}
                        </Link>
                      </td>
                      <td>{cohort.capacity}</td>
                      <td>{cohort.modeOfTeaching}</td>
                      <td>{cohort.startDate.split("T")[0]}</td>
                      <td>{cohort.endDate.split("T")[0]}</td>
                      <td>{cohort.city}</td>
                      <td>
                        <Link to={`/cohortedit/${cohort.cohortId}`}>Edit</Link>{" "}
                        |{" "}
                        <Link to="#" onClick={handleShow}>
                          {" "}
                          Archive{" "}
                        </Link>
                        <Modal
                          key={cohort.cohortId}
                          show={show}
                          onHide={handleClose}
                        >
                          <Modal.Body>Retire: Are you sure?</Modal.Body>
                          <Modal.Footer>
                            <Button variant="secondary" onClick={handleClose}>
                              No
                            </Button>
                            <Button
                              variant="primary"
                              onClick={() => onArchive(cohort.cohortId)}
                            >
                              Yes
                            </Button>
                          </Modal.Footer>
                        </Modal>
                      </td>
                    </tr>
                  ))}
              </tbody>
            </Table>
            <button type="button" className="btn btn-link" onClick={goBack}>
              Back
            </button>{" "}
            <Link to="/cohortcreate">
              <Button className="float-right mr-3">Create Cohort</Button>
            </Link>
            <Link to="/registeruser">
              <Button className="float-right mr-3" type="button">
                Register Users
              </Button>
            </Link>
            <Link to="/managecourse">
              <Button className="float-right mr-3">Manage Course</Button>{" "}
            </Link>
          </Container>
        </React.Fragment>
      )}
    </React.Fragment>
  );
};

export default CohortSummaryInstructor;
