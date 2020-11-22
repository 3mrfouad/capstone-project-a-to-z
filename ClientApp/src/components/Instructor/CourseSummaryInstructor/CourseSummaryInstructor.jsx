import React from 'react';
import {Table,Container} from 'react-bootstrap'
const CourseSummaryInstructor = () => {
    return ( 
      <React.Fragment>
        <Container>
            <Table>
            <thead>
    <tr>
      <th>Course Name</th>
      <th>Description</th>
      <th>Duration</th>
      <th>Cohort</th>
      <th>Instructor</th>
      <th>Homework</th>
      <th>Actions</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>React.js</td>
      <td>React Basics</td>
      <td>10</td>
      <td>4.2</td>
      <td>Instructor A</td>
      <td><a href="#">Homework</a> </td>
      <td>Edit | Archive</td>
    </tr>
    <tr>
    <td>CSS</td>
      <td>CSS Basics</td>
      <td>10</td>
      <td>4.2</td>
      <td>Instructor B</td>
      <td><a href="#">Homework</a> </td>
      <td>Edit | Archive</td>
    </tr>
    <tr>
    <td>HTML</td>
      <td>HTML Basics</td>
      <td>10</td>
      <td>4.2</td>
      <td>Instructor B</td>
      <td> <a href="#">Homework</a> </td>
      <td>Edit | Archive</td>
    </tr>
  </tbody>
            </Table>
        </Container>
        </React.Fragment>
     );
}
 
export default CourseSummaryInstructor;