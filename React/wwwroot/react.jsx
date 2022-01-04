/*
class TableHeader extends React.Component
{
    render()
    {
        return (
            <thead className="tableHeader">
                <tr>
                    <th>Nr.</th>
                    <th>Name:</th>
                    <th>City:</th>
                    <th>PhoneNumber:</th>
                    <th>Languages:</th>
                    <th></th>
                </tr>
            </thead>
        );
    }
}
*/

class TableHeader extends React.Component {
    render() {
        return (
            <div className="tableHeader row">
                <div className="col-lg-1">Nr.</div>
                <div className="col-lg-2">Name:</div>
                <div className="col-lg-2">City:</div>
                <div className="col-lg-2">PhoneNumber:</div>
                <div className="col-lg-3">Languages:</div>
                <div className="col-lg-2"></div>
            </div>
        );
    }
}

/*
class PersonDetails extends React.Component
{
    render()
    {
        return (
            <tbody>
                {
                    this.props.people.map(
                        person =>
                            <tr className={person.rowClass} key={person.id}>
                                <td>{person.itemIndex + 1}</td>
                                <td>{person.name}</td>
                                <td>{person.city}</td>
                                <td>{person.phoneNumber}</td>
                                <td>{person.languages}</td>
                                <td><input type="button" className="formButton" onClick={() => this.props.deletePerson(person.id)} value="Delete" /></td>
                            </tr>
                    );
                }
            </tbody>
        );
    }
}
*/

class PersonDetails extends React.Component
{
    render()
    {
        return (
            this.props.people.map(
                person =>
                    <div className={person.rowClass + " row"}  key={person.id}>
                        <div className="col-lg-1">{person.itemIndex + 1}</div>
                        <div className="col-lg-2">{person.name}</div>
                        <div className="col-lg-2">{person.city}</div>
                        <div className="col-lg-2">{person.phoneNumber}</div>
                        <div className="col-lg-3">{person.languages}</div>
                        <div className="col-lg-2"><input type="button" className="formButton pull-right" onClick={() => this.props.deletePerson(person.id)} value="Delete" /></div>
                    </div>
            )
        );
    }
}

class AddPersonForm extends React.Component
{
    constructor(props)
    {
        super(props);
        this.state = { name: '', phoneNumber: '', cityId: '1', languages: [], cities: [], availableLanguages: [] };
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handlePhoneNumberChange = this.handlePhoneNumberChange.bind(this);
        this.handleCityChange = this.handleCityChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleLanguagesChange = this.handleLanguagesChange.bind(this);
    }

    componentDidMount()
    {
        this.fetchCityListFromServer();
        this.fetchLanguageListFromServer();
    }

    fetchCityListFromServer()
    {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.fetchCityListURL, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ cities: data });
        };
        xhr.send();
    }

    fetchLanguageListFromServer()
    {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.fetchLanguagesListURL, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ availableLanguages: data });
        };
        xhr.send();
    }

    handleNameChange(e)
    {
        this.setState({ name: e.target.value });
    }

    handlePhoneNumberChange(e)
    {
        this.setState({ phoneNumber: e.target.value });
    }

    handleCityChange(e)
    {
        this.setState({ cityId: e.target.value });
    }

    handleLanguagesChange(e)
    {
        let value = Array.from(
            e.target.selectedOptions,
            (option) => option.value
        );
        this.setState({
            languages: value,
        });

    }

    handleSubmit(e)
    {
        e.preventDefault();
        const name = this.state.name.trim();
        const cityId = this.state.cityId;

        const phoneNumber = this.state.phoneNumber.trim();
        if (!name || !phoneNumber)
        {
            return;
        }
        this.props.onAddPersonSubmit({ name: name, phoneNumber: phoneNumber, cityId: cityId, languages: this.state.languages });
        this.setState({ name: '', phoneNumber: '', cityId: '1', languages: [] });
    }
    render()
    {
        const selectCityOptionList = this.state.cities.map(city =>
            <option key={city.id} value={city.id}>{city.name}</option>
        );
        const selectLanguagesOptionList = this.state.availableLanguages.map(lang =>
            <option key={lang.id} value={lang.id}>{lang.name}</option>
        );

        return (
            <form id="AddPersonForm" className="form" onSubmit={this.handleSubmit}>
                <label>&nbsp;Name:</label>
                <input
                    className="formInputField"
                    type="text"
                    placeholder="Name"
                    value={this.state.name}
                    onChange={this.handleNameChange}
                />
                <label>&nbsp;Phone number:</label>
                <input
                    className="formInputField"
                    type="text"
                    placeholder="Phone number"
                    value={this.state.phoneNumber}
                    onChange={this.handlePhoneNumberChange}
                />
                <label>&nbsp;City:</label>
                <select className="formInputField" value={this.state.cityId} onChange={this.handleCityChange}>
                    {selectCityOptionList}
                </select>
                <label>&nbsp;Languages:</label>
                <select className="formInputField" value={this.state.languages} multiple={true} size="3" onChange={this.handleLanguagesChange}>
                    {selectLanguagesOptionList}
                </select>
                <input className="formButton" type="submit" value="Add" />
            </form>
        );
    }
}

class PeopleList extends React.Component
{
    constructor(props)
    {
        super(props)
        this.state = { people: [] };

        this.fetchPeopleListFromServer = this.fetchPeopleListFromServer.bind(this);
        this.deletePerson = this.deletePerson.bind(this);
        this.handleAddPersonSubmit = this.handleAddPersonSubmit.bind(this);
    }

    componentDidMount()
    {
        this.fetchPeopleListFromServer();
    }

    fetchPeopleListFromServer()
    {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.fetchPeopleListURL, true);
        xhr.onload = () =>
        {
            const data = JSON.parse(xhr.responseText);
            this.setState({ people: data });
        };
        xhr.send();
    }

    handleAddPersonSubmit(person)
    {
        const data = new FormData();
        data.append('Name', person.name);
        data.append('PhoneNumber', person.phoneNumber);
        data.append('CityId', person.cityId);
        data.append('Languages', person.languages);

        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.addPersonURL, true);
        xhr.onload = () => this.fetchPeopleListFromServer();
        xhr.send(data);
    }

    deletePerson(id)
    {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.deletePersonURL + "?id=" + id, true);
        xhr.onload = () =>
        {
            this.fetchPeopleListFromServer();
        };

        xhr.send();
    }

    render()
    {
        return (
            <div className="container my-container">
                <div className="row">
                    <AddPersonForm onAddPersonSubmit={this.handleAddPersonSubmit} fetchCityListURL={this.props.fetchCityListURL}
                        fetchLanguagesListURL={this.props.fetchLanguagesListURL}
                    />
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <span>&nbsp;</span>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-10">
                        <h3>People list:</h3>
                    </div>
                    <div className="col-lg-2">
                        <input type="button" className="formButton pull-right" onClick={this.fetchPeopleListFromServer} value="Refresh" />
                    </div>
                </div>
                <TableHeader />
                <PersonDetails people={this.state.people} deletePerson={this.deletePerson} />
                <div className="tableHeader row">
                    <div className="col-lg-10">
                        <span>&nbsp;</span>
                    </div>
                    <div className="col-lg-2">
                        <input type="button" className="formButton pull-right" onClick={this.fetchPeopleListFromServer} value="Refresh" />
                    </div>
                 </div>
            </div>
        );
    }      
}

ReactDOM.render(
    <PeopleList
        fetchPeopleListURL="/React/GetPeopleList"
        addPersonURL="/React/AddPerson"
        fetchCityListURL="/React/GetCityList"
        fetchLanguagesListURL="/React/GetLanguageList"
        deletePersonURL="/React/DeletePerson"
    />,
    document.getElementById('content')
);