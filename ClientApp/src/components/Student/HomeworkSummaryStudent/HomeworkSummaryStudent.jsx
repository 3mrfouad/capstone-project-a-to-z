import React from 'react';
import {Table,Container, Button} from 'react-bootstrap'
const HomeworkSummaryStudent = () => {
    return ( 
      <React.Fragment>
         <Container>
            <Table>
            <thead>
    <tr>
      <th>Homework Name</th>
      <th>Due Date</th>
      <th>GitHub</th>
      <th>Category</th>
      <th>Details</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>React101</td>
      <td>2020-11-19</td>
      <td> <a href="#">GitHubLink</a> </td>
      <td>Practice</td>
      <td>View | Grades</td>
    </tr>
    <tr>
    <td>React102</td>
      <td>2020-11-29</td>
      <td><a href="#">GitHubLink</a></td>
      <td>Assignment</td>
      <td>View | Grades</td>
    </tr>
  </tbody>
            </Table>
            <Button>Back</Button>
        </Container>
        </React.Fragment>
     );
}
 
export default HomeworkSummaryStudent;