import React, { useEffect, useState } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
    getAllCourses,
    getAllInstructors,
    assignCourse,
} from "../../../actions/instructorActions";

const CourseAssign = ({ match }) => {
    const cohortId = match.params.id;
    const dispatch = useDispatch();
    const [courseId, setCourseId] = useState("");
    const [instructorId, setInstructorId] = useState("");
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");
    const [resourcesLink, setResourcesLink] = useState("");
    const { loading, courses } = useSelector((state) => state.getAllCourses);
    const { instructors } = useSelector((state) => state.getAllInstructors);
    const { success } = useSelector((state) => state.courseAssign);
    //(1) Add validation states
    const [validated, setValidated] = useState(false);
    const [invalidDatesBL, setInvalidDatesBl] = useState(false);
    //----------------------------


    useEffect(() => {
        dispatch(getAllCourses());
        dispatch(getAllInstructors());
    }, []);
    const submitHandler = (e) => {
        //(2) Add form validation condition block if-else
        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        setValidated(true);
        //(3) Add business logic
        if (endDate === "" || startDate === "" || Date.parse(endDate) < Date.parse(startDate)) {
            e.preventDefault();
            Date.parse(endDate) < Date.parse(startDate) ? setInvalidDatesBl(true) : setInvalidDatesBl(false);
            setEndDate("");
        } else {
            e.preventDefault();
            setInvalidDatesBl(false);
            e.preventDefault();
            dispatch(
                assignCourse
                    ({
                        cohortId,
                        courseId,
                        instructorId,
                        startDate,
                        endDate,
                        resourcesLink,
                    })
            );
        }
    };
    return (
        <React.Fragment>
            <Container>
                <Row className="justify-content-md-center">
                    <Col xs={12} md={6}>
                        <h2>Course</h2>
                        <Form noValidate validated={validated} onSubmit={submitHandler}>
                            <Form.Group controlId="name">
                                <Form.Label>Course Name</Form.Label>
                                <Form.Control
                                    as="select"
                                    value={courseId}
                                    onChange={(e) => setCourseId(e.target.value)}
                                >
                                    <option value="">select</option>
                                    {courses.map((course, index) => (
                                        <option value={course.courseId} key={index}>
                                            {course.name}
                                        </option>
                                    ))}
                                </Form.Control>
                            </Form.Group>

                            <Form.Group controlId="instructor">
                                <Form.Label>Instructor</Form.Label>
                                <Form.Control
                                    as="select"
                                    value={instructorId}
                                    onChange={(e) => setInstructorId(e.target.value)}
                                >
                                    <option value="">select</option>
                                    {instructors.map((instructor, index) => (
                                        <option value={instructor.userId} key={index}>
                                            {instructor.name}
                                        </option>
                                    ))}
                                </Form.Control>
                            </Form.Group>
                            <Form.Group controlId="startdate">
                                <Form.Label>Start Date</Form.Label>
                                <Form.Control
                                    required
                                    type="date"
                                    value={startDate}
                                    onChange={(e) => setStartDate(String(e.target.value))}
                                ></Form.Control>
                            </Form.Group>
                            <Form.Group controlId="enddate">
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
                            <Form.Group controlId="hours">
                                <Form.Label>Hours</Form.Label>
                                <Form.Control disabled></Form.Control>
                            </Form.Group>
                            <Form.Group controlId="description">
                                <Form.Label>Description</Form.Label>
                                <Form.Control disabled></Form.Control>
                            </Form.Group>
                            <Form.Group controlId="link">
                                <Form.Label>Resource Link</Form.Label>
                                <Form.Control
                                    type="url"
                                    value={resourcesLink}
                                    onChange={(e) => setResourcesLink(e.target.value)}
                                ></Form.Control>
                                <Form.Control.Feedback type="invalid">
                                    Please enter a valid URL.
                                </Form.Control.Feedback>
                            </Form.Group>
                            <button type="button" className="btn btn-link">
                                Back
              </button>
                            <Button type="submit" variant="primary" className="float-right">
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

export default CourseAssign;
