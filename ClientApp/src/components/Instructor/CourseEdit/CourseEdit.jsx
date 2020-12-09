import React, { useState, useEffect } from "react";
import { Form, Button, Row, Col, Container } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { editCourse, getCourse } from "../../../actions/instructorActions";
import Loader from "../../shared/Loader/Loader";
import Message from "../../shared/Message/Message";

const CourseEdit = ({ match, history }) => {
    const courseId = match.params.id;
    const [courseName, setCourseName] = useState("");
    const [hours, setHours] = useState("");
    const [description, setDescription] = useState("");
    const dispatch = useDispatch();

    /* Validation states*/

    const [validated, setValidated] = useState(false);

    /*Anti-tamper validation - States and Variables*/

    const [validData, setValidData] = useState(false);
    const [formSubmitted, setFormSubmitted] = useState(false);
    const [formDataChange, setFormDataChange] = useState(false);
    let validFormData = false;
    let formSubmitIndicator = false;
    const courseEdit = useSelector((state) => state.courseEdit);
    const getCourseDetail = useSelector((state) => state.getCourse);
    const { success } = courseEdit;
    const { loading, error, course } = getCourseDetail;

    /*Anti-tamper validation - Validate (parameters)*/

    function Validate(courseId, courseName, hours, description) {
        formSubmitIndicator = true;

        try {
            courseId = courseId.trim().toLowerCase();
            courseName = courseName.trim().toLowerCase();
            hours = String(hours).trim();
            description = description.trim().toLowerCase();

            if (!courseId) {
                validFormData = false;
            } else if (courseId < 0 || courseId > 2147483647) {
                validFormData = false;
            } else if (!courseName) {
                validFormData = false;
            } else if (courseName.Length > 50) {
                validFormData = false;
            } else if (!hours) {
                validFormData = false;
            } else if (parseFloat(hours) > 999.99 || parseFloat(hours) < 0) {
                validFormData = false;
            } else if (!description) {
                validFormData = false;
            } else if (description.Length > 250) {
                validFormData = false;
            } else {
                validFormData = true;
            }
        } catch (Exception) {
            validFormData = false;
        }
    }

    useEffect(() => {
            if (success) {
                dispatch({
                    type: "COURSE_EDIT_RESET",
                });
            } else {
                if (
                    !course ||
                        !course.name ||
                        course.courseId != courseId ||
                        formDataChange
                ) {
                    {
                        dispatch(getCourse(courseId));
                        setFormDataChange(false);
                    }
                } else {
                    setCourseName(course.name);
                    setDescription(course.description);
                    setHours(course.durationHrs);
                }
            }
        },
        [dispatch, course, success]);

    const submitHandler = (e) => {

        /*Form validation condition block */

        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        setValidated(true);
        e.preventDefault();

        /* Anti-tamper validation - calling Validate*/

        Validate(courseId, courseName, hours, description);
        if (validFormData) {
            setValidData(validFormData);
            dispatch(
                editCourse({
                    courseId,
                    name: courseName,
                    description,
                    durationHrs: hours,
                })
            );
        } else {
            /*Anti-tamper validation - Alert message conditions*/

            setValidData(validFormData);
        }

        /*Anti-tamper validation - Alert message conditions*/

        setFormSubmitted(formSubmitIndicator);
    };

    const goBack = () => {
        history.goBack();
    };

    return (
        <React.Fragment>
            {loading
                ? (
                    <Loader/>
                )
                : (
                    <Container>
                        <Row className="justify-content-md-center">
                            <Col xs={12} md={6}>
                                <h2> Update Course</h2>
                                {success &&
 (
     <Message variant="success">Course Updated Successfully</Message>
 )}
                                <p
                                    className={
                  formSubmitted
                    ? validData
                      ? !loading && error
                        ? "alert alert-danger"
                        : !loading && !error
                        ? "alert alert-success"
                        : ""
                      : "alert alert-danger"
                    : ""
                }
                                    role="alert">
                                    {formSubmitted
                    ? validData
                    ? !loading && error
                    ? `Unsuccessful attempt to update course. ${error.data}`
                    : !loading && !error
                    ? "Course was successfully updated"
                    : ""
                    : "Error: Form was submitted with invalid data fields"
                    : ""}
                                </p>
                                <Form noValidate validated={validated} onSubmit={submitHandler}>
                                    <Form.Group controlId="CourseName">
                                        <Form.Label>Course Name</Form.Label>

                                        <Form.Control
                                            required
                                            type="text"
                                            maxLength="50"
                                            value={courseName}
                                            onChange={(e) => {
                        setCourseName(e.target.value);
                        setFormDataChange(true);
                    }}>
                                        </Form.Control>
                                        <Form.Control.Feedback type="invalid">
                                            Please enter a course name.
                                        </Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group controlId="Hours">
                                        <Form.Label className="mr-5">Hours</Form.Label>

                                        <Form.Control
                                            required
                                            type="number"
                                            min={0}
                                            max={999.99}
                                            step="0.25"
                                            value={hours}
                                            onChange={(e) => {
                        setHours(String(e.target.value));
                        setFormDataChange(true);
                    }}>
                                        </Form.Control>
                                        <Form.Control.Feedback type="invalid">
                                            Please fill in the Hours field.
                                        </Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group controlId="Description">
                                        <Form.Label>Description</Form.Label>

                                        <Form.Control
                                            as="textarea"
                                            required
                                            maxLength="250"
                                            value={description}
                                            onChange={(e) => {
                        setDescription(e.target.value);
                        setFormDataChange(true);
                    }}>
                                        </Form.Control>
                                        <Form.Control.Feedback type="invalid">
                                            Please enter description for Course.
                                        </Form.Control.Feedback>
                                    </Form.Group>
                                    <button type="button" className="btn btn-link" onClick={goBack}>
                                        Back
                                    </button>{" "}
                                    <Button type="submit" className="float-right">
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

export default CourseEdit;