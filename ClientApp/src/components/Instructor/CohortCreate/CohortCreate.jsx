import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { createCohort } from "../../../actions/instructorActions";

const CohortCreate = () => {
    const [name, setName] = useState("");
    const [capacity, setCapacity] = useState("");
    const [modeOfTeaching, setModeOfTeaching] = useState("");
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");
    const [city, setCity] = useState("");

    //(1) Add validation states
    const [validated, setValidated] = useState(false);
    const [invalidDatesBL, setInvalidDatesBl] = useState(false);
    //----------------------------

    useEffect(() => {
        // get cohort by id
        // populate the cohort data in here
    }, []);
    const cohortCreate = useSelector((state) => state.cohortCreate);
    const { loading, error, cohort, success } = cohortCreate;
    const dispatch = useDispatch();

    const submitHandler = (e) => {

        //(2) Add form validation condition block if-else
        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        setValidated(true);

        //(3) Add business logic
        if (endDate === "" || startDate==="" || Date.parse(endDate) < Date.parse(startDate)) {
            e.preventDefault();
            Date.parse(endDate) < Date.parse(startDate) ? setInvalidDatesBl(true) : setInvalidDatesBl(false);
            setEndDate("");
        }
        else {

            setInvalidDatesBl(false);
            //----------------------------


            /*
             ---------------------------------------------------------------------------------
             Post HTML5 regular validation (Frontend validation / BL)
             if (ValidationCreateCohort(name,capacity,city,modeOfTeaching,startDate,endDate))
            {

            }
             else
            {
                //Return error on front\end view
            }

// Do it but test first the time require Amr(1), Ayesha (1), Atinder (1)
// Dont it ... If time consuming
            ----------------------------------------------------------------------------------
            */

            e.preventDefault();
            console.log("create cohort");
            dispatch(
                createCohort({
                    name,
                    capacity,
                    city,
                    modeOfTeaching,
                    startDate,
                    endDate,
                })
            );
        }
    };
    return (
        <React.Fragment>
            <Container>
                <Row className="justify-content-md-center">
                    <Col xs={12} md={6}>
                        <h2>Cohort</h2>
                        <Form noValidate validated={validated} onSubmit={submitHandler}>
                            <Form.Group controlId="name">
                                <Form.Label>Name</Form.Label>
                                {
                                    /*
                                     * (4) Add required to all required input fields
                                     * (5) For type = "text", use maxlength ={50} i.e., according to the actual length from ERD
                                     * (5) If type is not "text", add String(e.target.value) to the onchange
                                     * (6) If type is "number", add min={0}, and max={999} i.e., according to actual range from ERD
                                     * (7) For dropdown menu, use as="select", and add one empty option above other options <option></option>
                                     */
                                }
                                <Form.Control
                                    required
                                    type="text"
                                    maxlength ="50"
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                ></Form.Control>
                                {
                                    /*(8) Add Form control feedback.*/
                                }
                                <Form.Control.Feedback type="invalid">
                                    Please enter a cohort name.
                                </Form.Control.Feedback>

                                {/*---------------------------------------*/}
                            </Form.Group>

                            <Form.Group controlId="Capacity">
                                <Form.Label>Capacity</Form.Label>
                                <Form.Control
                                    type="number"
                                    min={0}
                                    max={999}
                                    step="1"
                                    value={capacity}
                                    onChange={(e) => setCapacity(String(e.target.value))}
                                ></Form.Control>
                            </Form.Group>
                            <Form.Group controlId="Mode of Teaching">
                                <Form.Label>Mode of Teaching</Form.Label>
                                <Form.Control
                                    as="select"
                                    required
                                    value={modeOfTeaching}
                                    onChange={(e) => setModeOfTeaching(String(e.target.value))}
                                ><option></option>
                                    <option>Online</option>
                                    <option>In Person</option>
                                </Form.Control>
                                <Form.Control.Feedback type="invalid">
                                    Please choose a mode of teaching.
                                </Form.Control.Feedback>
                            </Form.Group>
                            <Form.Group controlId="Start Date">
                                <Form.Label>Start Date</Form.Label>
                                <Form.Control
                                    required
                                    type="date"
                                    value={startDate}
                                    onChange={(e) => setStartDate(String(e.target.value))}
                                ></Form.Control>
                                <Form.Control.Feedback type="invalid">
                                    Please choose a start date.
                                </Form.Control.Feedback>
                            </Form.Group>
                            <Form.Group controlId="End Date">
                                <Form.Label>End Date</Form.Label>
                                <Form.Control
                                    required
                                    type="date"
                                    value={endDate}
                                    onChange={(e) => setEndDate(String(e.target.value))}
                                ></Form.Control>
                                <Form.Control.Feedback type="invalid">
                                    Please choose an end date. 
                                </Form.Control.Feedback>
                                {/* (9) Add business logic validation message. */}
                                <p className="text-danger small">{invalidDatesBL ? "End date can't be before start date" : ""}</p>
                                {/*---------------------------------------*/}
                            </Form.Group>
                            <Form.Group controlId="City">
                                <Form.Label>City</Form.Label>
                                <Form.Control as="select"
                                    required
                                    value={city}
                                    onChange={(e) => setCity(e.target.value)}
                                >
                                    <option></option>
                                    <option>Edmonton</option>
                                    <option>Calgary</option>
                                    <option>Other</option>
                                </Form.Control>
                                <Form.Control.Feedback type="invalid">
                                    Please choose a city.
                                </Form.Control.Feedback>
                            </Form.Group>
                            <a href="">Back</a>
                            <Button className="float-right" type="submit" variant="primary">
                                {" "}
                Save
              </Button>
                        </Form>
                    </Col>
                </Row>
            </Container>
        </React.Fragment>
    );
};

export default CohortCreate;
