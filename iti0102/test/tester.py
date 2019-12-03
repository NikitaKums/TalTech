import orwell_1984


def test():
    ip = orwell_1984.InnerParty()
    op = orwell_1984.OuterParty()

    c = orwell_1984.Citizen("Winston Smith", op, "under surveillance")
    c1 = orwell_1984.Citizen("Julia", op, "under surveillance")

    assert (c.get_name()) == 'Winston Smith'
    assert str(c.get_party()) == 'Outer party'
    assert (c.get_status()) == 'under surveillance'
    assert str(c) == 'BIG BROTHER IS WATCHING YOU, Winston Smith'
    assert str(c1) == 'BIG BROTHER IS WATCHING YOU, Julia'
    c.set_party(ip)
    assert str(c.get_party()) == 'Inner party'
    assert len(ip.get_party_members()) == 1
    assert len(op.get_party_members()) == 1
    assert not (c in op.get_party_members())
    c.set_party(op)
    assert str(c.get_party()) == 'Outer party'
    assert len(op.get_party_members()) == 2

    c2 = orwell_1984.Citizen("O'Brien", ip)
    assert (c2.get_name()) == "O'Brien"
    assert str(c2.get_party()) == 'Inner party'
    assert (c2.get_status()) == 'citizen'

    c3 = orwell_1984.Citizen("Syme", op, "nonperson")
    assert (c3.get_name()) is None
    assert (c3.get_party()) is None
    assert (c3.get_status()) == 'nonperson'

    c4 = orwell_1984.Citizen("Smb", op, "prole")

    assert (c4.get_name()) == 'Smb'
    assert (c4.get_party()) is None
    assert (c4.get_status()) == 'prole'

    assert len(ip.get_party_members()) == 1
    assert len(op.get_party_members()) == 2
    assert str(ip) == 'Inner party'
    assert str(op) == 'Outer party'
    assert (ip.get_privileges()) == 'Everything'
    assert (op.get_privileges()) is None

    assert (ip.get_slogan()) == 'WAR IS PEACE\nFREEDOM IS SLAVERY\nIGNORANCE IS STRENGTH'
