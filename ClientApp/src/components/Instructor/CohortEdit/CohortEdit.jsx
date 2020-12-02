import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { editCohort, cohortGet } from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";

const CohortEdit = ({ match, history }) => {
  const cohortId = match.params.id;
  console.log(cohortId);
  const [name, setName] = useState("");
  const [capacity, setCapacity] = useState("");
  const [modeOfTeaching, setModeOfTeaching] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [city, setCity] = useState("");
  const dispatch = useDispatch();

  const cohortEdit = useSelector((state) => state.cohortEdit);
  const cohortGetState = useSelector((state) => state.cohortGetState);
  const { error, success } = cohortEdit;
  const { cohort, loading } = cohortGetState;

  useEffect(() => {
    if (!cohort || !cohort.name) {
      dispatch(cohortGet(cohortId));
    } else {
      setName(cohort.name);
      setCapacity(cohort.capacity);
      setCity(cohort.city);
      setEndDate(cohort.endDate);
      setStartDate(cohort.startDate);
      setModeOfTeaching(cohort.modeOfTeaching);
    }
  }, [dispatch, cohort]);

  const submitHandler = (e) => {
    e.preventDefault();
    console.log("update cohort");
    dispatch(
      editCohort({
        cohortId,
        name,
        capacity,
        city,
        modeOfTeaching,
        startDate,
        endDate,
      })
    );
  };
  return (
    <React.Fragment>
      {loading ? (
        <Loader />
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h2>Cohort</h2>
              <Form onSubmit={submitHandler}>
                <Form.Group controlId="name">
                  <Form.Label>Cohort Name</Form.Label>
                  <Form.Control
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                  ></Form.Control>
                </Form.Group>

                <Form.Group controlId="Capacity">
                  <Form.Label>Capacity</Form.Label>
                  <Form.Control
                    type="text"
                    value={capacity}
                    onChange={(e) => setCapacity(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Mode of Teaching">
                  <Form.Label>Mode of Teaching</Form.Label>
                  <Form.Control
                    type="text"
                    value={modeOfTeaching}
                    onChange={(e) => setModeOfTeaching(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Start Date">
                  <Form.Label>Start Date</Form.Label>
                  <Form.Control
                    type="text"
                    value={startDate}
                    onChange={(e) => setStartDate(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="End Date">
                  <Form.Label>End Date</Form.Label>
                  <Form.Control
                    type="text"
                    value={endDate}
                    onChange={(e) => setEndDate(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="City">
                  <Form.Label>City</Form.Label>
                  <Form.Control
                    type="text"
                    value={city}
                    onChange={(e) => setCity(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <a href="">Back</a>
                <Button type="submit" variant="primary" className="float-right">
                  {" "}
                  Save
                </Button>
              </Form>
            </Col>
          </Row>
        </Container>
      )}
    </React.Fragment>
  );
};

export default CohortEdit;
