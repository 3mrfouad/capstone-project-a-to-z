import React, { useState, useEffect } from "react";
import { Table, Container, Button, Form, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import {
  getAllCourses,
  getAllInstructors,
  createHomeworkInstructor,
} from "../../../actions/instructorActions";

const HomeworkCreateInstructor = ({ match, history }) => {
  const cohortId = match.params.id;
  // const courseId = match.params.courseId;

  const [courseId, setCourseId] = useState("");
  const [instructorId, setInstructorId] = useState("");
  const [isAssignment, setIsAssignment] = useState("");
  const [title, setTitle] = useState("");
  const [avgCompletionTime, setAvgCompletionTime] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [releaseDate, setReleaseDate] = useState("");
  const [documentLink, setDocumentLink] = useState("");
  // const [isChallenge, setIsChallenge] = useState(false);
  // const [criteria, setCriteria] = useState("");
  // const [weight, setWight] = useState("");
  const [gitHubClassRoomLink, setGitHubClassRoomLink] = useState("");
  //(1) Add validation states (@Atinder)
   const [validated, setValidated] = useState(false);  
   let temp = false; 
  //----------------------------
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(getAllCourses());
    dispatch(getAllInstructors());
  }, []);

  const { loading, courses } = useSelector((state) => state.getAllCourses);
  const { instructors } = useSelector((state) => state.getAllInstructors);

  const goBack = () => {
    history.goBack();
  };
  const handleSubmit = (e) => {
    //(2) Add form validation condition block if-else
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.preventDefault();
      e.stopPropagation();
    }
    console.log("pass initial validation 100");
    setValidated(true);
    //----------------------------

    e.preventDefault();
    dispatch(
      createHomeworkInstructor({
        courseId,
        instructorId,
        cohortId,
        // isAssignment,
        title,
        avgCompletionTime,
        dueDate,
        releaseDate,
        documentLink,
        gitHubClassRoomLink,
      })
    );
  };

  return (
    <React.Fragment>
      {loading ? (
        <h2>Loading</h2>
      ) : (
        <Container>
          <Row className="justify-content-md-center">
            <Col xs={12} md={6}>
              <h3>Homework</h3>
              <Form noValidate validated={validated} onSubmit={handleSubmit}>
                <Form.Group controlId="title">
                  <Form.Label>Title</Form.Label>
                  <Form.Control
                    required
                    type="text"
                    maxLength="100"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                  Please enter Title of Homework.(Max 100 characters allowed).
                </Form.Control.Feedback>

                {/*---------------------------------------*/}
                </Form.Group>
                <Form.Group controlId="Course">
                  <Form.Label>Course</Form.Label>
                  <Form.Control
                    as="select"
                    required
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
                  {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                  Please select a course for this homework.
                </Form.Control.Feedback>

                {/*---------------------------------------*/}
                </Form.Group>

                <Form.Group controlId="instructor">
                  <Form.Label>Instructor</Form.Label>
                  <Form.Control
                    as="select"
                    required
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
                  {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                  Please select an Instructor for this homework.
                </Form.Control.Feedback>

                {/*---------------------------------------*/}
                </Form.Group>

                <Form.Group controlId="Avg Completion Time">
                  <Form.Label>Avg Completion Time</Form.Label>
                  <Form.Control
                    type="number"
                    min={0}
                    max={999.99}
                    step="0.25"
                    value={avgCompletionTime ? avgCompletionTime : 0}
                    onChange={(e) =>
                      setAvgCompletionTime(String(e.target.value))
                    }
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                  Please enter average completion time between 0 and 999.99 inclusive
                </Form.Control.Feedback>

                {/*---------------------------------------*/}
                </Form.Group>                

                <Form.Group controlId="Release Date">
                  <Form.Label>Release Date</Form.Label>
                  <Form.Control
                    type="date"
                    value={releaseDate}
                    onChange={(e) => setReleaseDate(String(e.target.value))}
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                  Please enter Date in format: yyyy/mm/dd. 
                </Form.Control.Feedback>

                {/*---------------------------------------*/}
                </Form.Group>

                <Form.Group controlId="Due Date">
                  <Form.Label>Due Date</Form.Label>
                  <Form.Control
                    type="date"
                    min={releaseDate}
                    value={dueDate}
                    onChange={(e) => setDueDate(String(e.target.value))}
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                  Please enter Date in format: yyyy/mm/dd. Due Date can not be set before Release Date.
                </Form.Control.Feedback>

                {/*---------------------------------------*/}
                </Form.Group>

                <Form.Group controlId="DocLink">
                  <Form.Label>DocLink</Form.Label>
                  <Form.Control
                    type="url"
                    maxLength="250"
                    value={documentLink}
                    placeholder="example: https://www.google.com "
                    onChange={(e) => setDocumentLink(String(e.target.value))}
                  ></Form.Control>
                  {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                  <p>Please enter Document Link in format: http|https://yourLink.</p>
                  <p>Max 250 characters allowed.</p>
                </Form.Control.Feedback>

                {/*---------------------------------------*/}
                </Form.Group>
                <Form.Group controlId="GitHubLink">
                  <Form.Label>GitHubLink</Form.Label>
                  <Form.Control
                    type="url"
                    maxLength="250"
                    value={gitHubClassRoomLink}
                    placeholder="example: https://www.google.com"
                    onChange={(e) =>
                      setGitHubClassRoomLink(String(e.target.value))
                    }
                  ></Form.Control>
                   {/*(8) Add Form control feedback.*/}
                <Form.Control.Feedback type="invalid">
                <p>Please enter Document Link in format: http|https://yourLink.</p>
                  <p>Max 250 characters allowed.</p>
                </Form.Control.Feedback>

                {/*---------------------------------------*/}
                </Form.Group>

                {/* <h3>Rubric</h3>
                <Form.Group controlId="Challenge">
                  <Form.Label>Challenge</Form.Label>
                  <Form.Control
                    type="checkbox"
                    label="Challenge"
                    value={isChallenge}
                    onChange={(e) => setIsChallenge(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Criteria">
                  <Form.Label>Criteria</Form.Label>
                  <Form.Control
                    type="text"
                    value={criteria}
                    onChange={(e) => setCriteria(e.target.value)}
                  ></Form.Control>
                </Form.Group>
                <Form.Group controlId="Weight">
                  <Form.Label>Weight</Form.Label>
                  <Form.Control
                    type="number"
                    min={0}
                    max={999}
                    step="1"
                    //   placeholder="Enter Description"
                    value={weight}
                    onChange={(e) => setWight(e.target.value)} //use String() - this is number
                  ></Form.Control>
                </Form.Group> */}
                <Button variant="primary" onClick={goBack}>
                  Back
                </Button>
                <Button type="submit" variant="primary" className="float-right">
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

export default HomeworkCreateInstructor;
